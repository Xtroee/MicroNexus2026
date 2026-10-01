using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct LevelNodeData
{
    public int stageIndex;
    public int levelInStage;
    public int globalOrdinal;
    public Vector2 anchoredPos;
    public NodeState state;
    public string sceneName;
    public string displayName;
    public BacteriaFrameSet bacteriaFrameSet;
}

[Serializable]
public struct StagePathStyle
{
    public float advanceSpacing;
    public float swingAmplitude;
    public float swingPeriodNodes;
    public float noiseAmplitude;
    public float noiseFrequency;
    public float edgeMarginFraction;
    public Color lineTint;

    public static StagePathStyle Default => new StagePathStyle
    {
        advanceSpacing = 220f,
        swingAmplitude = 0.28f,
        swingPeriodNodes = 5f,
        noiseAmplitude = 0.05f,
        noiseFrequency = 0.6f,
        edgeMarginFraction = 0.10f,
        lineTint = Color.white
    };
}

public class LevelMapController : MonoBehaviour
{
    [Header("Layout Rects")]
    [SerializeField] private RectTransform viewport;
    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform nodesContainer;
    [SerializeField] private RectTransform pathLineContainer;

    [Header("Prefabs")]
    [SerializeField] private LevelNodeView nodePrefab;
    [SerializeField] private Image lineSegmentPrefab;

    [Header("Path Style (parallel to GameData.Data.LevelsPerStage)")]
    [SerializeField] private StagePathStyle[] stageStyles;

    [Header("Sizing (horizontal scroll — path advances left to right)")]
    [SerializeField] private float nodeDiameter = 140f;
    [SerializeField] private float dotDiameter = 16f;
    [SerializeField] private float dotSpacing = 36f;
    [SerializeField] private float leftPadding = 160f;
    [SerializeField] private float rightPadding = 160f;

    [Header("Level Names (Stage 1, indexed by levelInStage - 1)")]
    [Tooltip("Display names carried over from the old Stage 1 Selection screen (disease names). Index 0 = Level 1, etc. Falls back to \"Level N\" if the array is shorter than the level count.")]
    [SerializeField] private string[] stage1LevelNames;

    [Header("Porthole Bacteria (Stage 1, indexed by levelInStage - 1)")]
    [Tooltip("Which bacteria color's animation shows inside each node's porthole - one of the bacteria actually spawned in that level. Matched by name against bacteriaFrameSets.")]
    [SerializeField] private string[] stage1LevelBacteriaColor;
    [SerializeField] private BacteriaFrameSet[] bacteriaFrameSets;

    [Header("Excluded Stages")]
    [Tooltip("Stage index (into GameData.Data.LevelsPerStage) to leave out of this map — e.g. Endless mode has its own separate entry point, not part of this numbered level path.")]
    [SerializeField] private int endlessStageIndex = 1;

    [Header("Navigation")]
    [SerializeField] private UIManager uiManager;

    private readonly List<LevelNodeData> nodes = new List<LevelNodeData>();
    private readonly List<GameObject> spawnedNodeViews = new List<GameObject>();
    private readonly List<GameObject> spawnedLineSegments = new List<GameObject>();

    private void Start()
    {
        RefreshMap();
    }

    private void RefreshMap()
    {
        // Viewport.rect isn't valid until Canvas/ScrollRect has done its first layout
        // pass, which hasn't happened yet if this runs from Start(). Force it now so
        // ComputeNodePosition doesn't read a zero/stale viewport height.
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(viewport);

        nodes.Clear();
        BuildNodeData();
        ClearSpawned();
        LayoutAndInstantiate();
        DrawPathLine();
        ResizeContent();
    }

    private void BuildNodeData()
    {
        int unlockedGlobal = GameData.Data.UnlockedLevel;
        int[] levelsPerStage = GameData.Data.LevelsPerStage;

        int globalOrdinal = 0;
        for (int stageIndex = 0; stageIndex < levelsPerStage.Length; stageIndex++)
        {
            if (stageIndex == endlessStageIndex)
                continue;

            StagePathStyle style = stageIndex < stageStyles.Length ? stageStyles[stageIndex] : StagePathStyle.Default;

            for (int levelInStage = 1; levelInStage <= levelsPerStage[stageIndex]; levelInStage++)
            {
                globalOrdinal++;

                // Lock feature temporarily disabled per pembimbing's request - every
                // level is playable, only the completed badge still reflects progress.
                // Restore the `: NodeState.Locked` branch once locking should return.
                NodeState state = globalOrdinal < unlockedGlobal ? NodeState.Completed
                    : NodeState.Unlocked;

                string sceneName = $"Level {levelInStage} Stage {stageIndex + 1}";

                string displayName = stageIndex == 0 && stage1LevelNames != null && levelInStage - 1 < stage1LevelNames.Length
                    ? stage1LevelNames[levelInStage - 1]
                    : $"Level {levelInStage}";

                Vector2 pos = ComputeNodePosition(globalOrdinal - 1, stageIndex, style);

                BacteriaFrameSet frameSet = stageIndex == 0
                    ? FindBacteriaFrameSet(levelInStage - 1)
                    : null;

                nodes.Add(new LevelNodeData
                {
                    stageIndex = stageIndex,
                    levelInStage = levelInStage,
                    globalOrdinal = globalOrdinal,
                    anchoredPos = pos,
                    state = state,
                    sceneName = sceneName,
                    displayName = displayName,
                    bacteriaFrameSet = frameSet
                });
            }
        }
    }

    private BacteriaFrameSet FindBacteriaFrameSet(int levelIndex)
    {
        if (stage1LevelBacteriaColor == null || levelIndex >= stage1LevelBacteriaColor.Length)
            return null;

        string colorName = stage1LevelBacteriaColor[levelIndex];
        if (string.IsNullOrEmpty(colorName) || bacteriaFrameSets == null)
            return null;

        foreach (BacteriaFrameSet set in bacteriaFrameSets)
            if (set != null && set.colorName == colorName)
                return set;

        return null;
    }

    private Vector2 ComputeNodePosition(int continuousIndex, int stageIndex, StagePathStyle style)
    {
        float viewportHeight = viewport.rect.height;
        float nodeRadius = nodeDiameter * 0.5f;
        float halfUsable = viewportHeight * (0.5f - style.edgeMarginFraction) - nodeRadius;

        float swing = Mathf.Sin(2f * Mathf.PI * continuousIndex / style.swingPeriodNodes)
            * style.swingAmplitude * viewportHeight * 0.5f;

        // Fixed per-stage seed so the same GameData state always produces the same
        // layout — otherwise the path would visibly jump between menu visits.
        float stageSeed = stageIndex * 17.3f;
        float noise = (Mathf.PerlinNoise(continuousIndex * style.noiseFrequency, stageSeed) - 0.5f) * 2f
            * style.noiseAmplitude * viewportHeight;

        float x = leftPadding + continuousIndex * style.advanceSpacing;
        float y = Mathf.Clamp(swing + noise, -halfUsable, halfUsable);

        return new Vector2(x, y);
    }

    private void LayoutAndInstantiate()
    {
        foreach (LevelNodeData nodeData in nodes)
        {
            LevelNodeView view = Instantiate(nodePrefab, nodesContainer);
            RectTransform rt = (RectTransform)view.transform;
            rt.anchoredPosition = nodeData.anchoredPos;

            view.Setup(nodeData, OnNodeClicked);

            spawnedNodeViews.Add(view.gameObject);
        }
    }

    private float GetVisualNodeRadius()
    {
        RectTransform imageBase = nodePrefab.transform.Find("Image_Base") as RectTransform;
        float diameter = imageBase != null ? imageBase.rect.width : nodeDiameter;
        return diameter * 0.5f;
    }

    private void DrawPathLine()
    {
        // nodeDiameter (91) sizes the node's hit-area/root RectTransform used for
        // ComputeNodePosition's edge margin - but the visible circle is a smaller child
        // ("Image_Base", currently 65) from an earlier "shrink to 65%" pass. Gaps must
        // clear the VISIBLE circle, not the bigger hit-area, or the node-to-dot gap
        // ends up visibly wider than the dot-to-dot gaps.
        float nodeRadius = GetVisualNodeRadius();
        float dotRadius = dotDiameter * 0.5f;

        for (int i = 0; i < nodes.Count - 1; i++)
        {
            Vector2 a = nodes[i].anchoredPos;
            Vector2 b = nodes[i + 1].anchoredPos;
            Vector2 direction = (b - a).normalized;
            float distance = Vector2.Distance(a, b);

            // Stretch between the two node EDGES (not centers) - this is what actually
            // gets filled with dots.
            float available = distance - nodeRadius * 2f;
            if (available <= 0f)
                continue;

            int stageIndex = nodes[i].stageIndex;
            Color tint = stageIndex < stageStyles.Length ? stageStyles[stageIndex].lineTint : Color.white;

            // Solve for a dot count + exact gap so every edge-to-edge gap along the
            // segment is equal - including the node-to-first-dot and last-dot-to-node
            // gaps, not just the gaps between dots. dotSpacing is the target gap size;
            // dotCount is picked so the exact gap comes out close to it, then the gap
            // is recomputed to fit `available` perfectly (n dots + (n+1) equal gaps).
            int dotCount = Mathf.Max(1, Mathf.RoundToInt((available - dotSpacing) / (dotDiameter + dotSpacing)));
            float gap = (available - dotCount * dotDiameter) / (dotCount + 1);

            for (int d = 1; d <= dotCount; d++)
            {
                float offset = nodeRadius + dotRadius + gap * d + (d - 1) * dotDiameter;
                Vector2 pos = a + direction * offset;

                Image dot = Instantiate(lineSegmentPrefab, pathLineContainer);
                RectTransform rt = dot.rectTransform;
                rt.anchoredPosition = pos;
                rt.sizeDelta = new Vector2(dotDiameter, dotDiameter);
                dot.color = tint;

                spawnedLineSegments.Add(dot.gameObject);
            }
        }
    }

    private void ResizeContent()
    {
        if (nodes.Count == 0)
            return;

        StagePathStyle lastStyle = nodes[^1].stageIndex < stageStyles.Length
            ? stageStyles[nodes[^1].stageIndex]
            : StagePathStyle.Default;

        float width = leftPadding + (nodes.Count - 1) * lastStyle.advanceSpacing + rightPadding;
        content.sizeDelta = new Vector2(width, content.sizeDelta.y);
    }

    private void ClearSpawned()
    {
        foreach (GameObject go in spawnedNodeViews)
            Destroy(go);
        spawnedNodeViews.Clear();

        foreach (GameObject go in spawnedLineSegments)
            Destroy(go);
        spawnedLineSegments.Clear();
    }

    private void OnNodeClicked(LevelNodeData nodeData)
    {
        if (nodeData.state == NodeState.Locked)
            return;

        uiManager.ChangeScene(nodeData.sceneName);
    }

    [ContextMenu("Debug: Unlock Next Level")]
    private void DebugUnlockNext()
    {
        GameData.Data.UnlockedLevel++;
        RefreshMap();
    }

    [ContextMenu("Debug: Unlock All Levels")]
    private void DebugUnlockAll()
    {
        int total = 0;
        for (int i = 0; i < GameData.Data.LevelsPerStage.Length; i++)
        {
            if (i == endlessStageIndex)
                continue;
            total += GameData.Data.LevelsPerStage[i];
        }

        GameData.Data.UnlockedLevel = total;
        RefreshMap();
    }

    [ContextMenu("Debug: Reset Locks")]
    private void DebugResetLocks()
    {
        GameData.Data.UnlockedLevel = 1;
        RefreshMap();
    }
}
