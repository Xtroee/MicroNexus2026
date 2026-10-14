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

    [Header("Path Pills (connector dots are drawn as the game's projectile pills)")]
    [Tooltip("Optional override. If empty, the set at Resources/LevelMapPills is used; if that is missing too, plain dots are drawn.")]
    [SerializeField] private Sprite[] pillSprites;
    [Tooltip("Edge length of each pill sprite, and the footprint used to space them along the path.")]
    [SerializeField] private float pillSize = 22f;
    [Tooltip("Fixed seed so the random colour/rotation layout is identical every time the map opens. Change it to reshuffle.")]
    [SerializeField] private int pillSeed = 12345;
    [Tooltip("Own up/down bob of the pills, in canvas units. Fades to zero toward each node so the pills next to a node move exactly with that node's floating.")]
    [SerializeField] private float pillFloatAmplitude = 3f;
    [SerializeField] private float pillFloatSpeed = 1.2f;

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

    // One entry per pill: where it sits on its segment, so LateUpdate can blend the
    // floating offsets of the two nodes it connects (spawnedNodeViews[i] <-> [i + 1]).
    // `along` runs 0..1 from the surface of the first node to the surface of the next one
    // (not centre to centre), so the chain's ends sit exactly on the nodes' own motion.
    private struct PillFloat
    {
        public RectTransform rt;
        public Vector2 basePos;
        public int fromNode;
        public float along;
        public float phase;
    }
    private readonly List<PillFloat> pillFloats = new List<PillFloat>();

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

        // Pills replace the plain dots when a sprite set is available. The footprint
        // (dotSize) drives the same equal-gap spacing maths either way.
        Sprite[] pills = ResolvePillSprites();
        bool usePills = pills.Length > 0;
        float dotSize = usePills ? pillSize : dotDiameter;
        float dotRadius = dotSize * 0.5f;
        System.Random rng = new System.Random(pillSeed);
        System.Random phaseRng = new System.Random(pillSeed + 1);   // separate stream: float phases never shift the colour/rotation layout
        int lastPill = -1;

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
            int dotCount = Mathf.Max(1, Mathf.RoundToInt((available - dotSpacing) / (dotSize + dotSpacing)));
            float gap = (available - dotCount * dotSize) / (dotCount + 1);

            for (int d = 1; d <= dotCount; d++)
            {
                float offset = nodeRadius + dotRadius + gap * d + (d - 1) * dotSize;
                Vector2 pos = a + direction * offset;

                Image dot = Instantiate(lineSegmentPrefab, pathLineContainer);
                RectTransform rt = dot.rectTransform;
                rt.anchoredPosition = pos;
                rt.sizeDelta = new Vector2(dotSize, dotSize);

                if (usePills)
                {
                    // Random colour (sprite) with no two identical pills side by side,
                    // and a random rotation. Seeded, so the layout is stable between visits.
                    int pick = rng.Next(pills.Length);
                    if (pills.Length > 1 && pick == lastPill)
                        pick = (pick + 1 + rng.Next(pills.Length - 1)) % pills.Length;
                    lastPill = pick;

                    dot.sprite = pills[pick];
                    dot.color = Color.white;
                    dot.preserveAspect = true;
                    dot.raycastTarget = false;
                    rt.localRotation = Quaternion.Euler(0f, 0f, (float)(rng.NextDouble() * 360.0));

                    pillFloats.Add(new PillFloat
                    {
                        rt = rt,
                        basePos = pos,
                        fromNode = i,
                        along = Mathf.Clamp01((offset - nodeRadius) / available),
                        phase = (float)(phaseRng.NextDouble() * Math.PI * 2.0)
                    });
                }
                else
                {
                    dot.color = tint;
                }

                spawnedLineSegments.Add(dot.gameObject);
            }
        }
    }

    // LateUpdate = after every FloatingIdle.Update, so the node offsets read here are
    // this frame's. Each pill follows the blend of its two nodes' floating (so the end
    // pills move with their node) plus its own bob, which is zero at the ends.
    private void LateUpdate()
    {
        if (pillFloats.Count == 0)
            return;

        for (int k = 0; k < pillFloats.Count; k++)
        {
            PillFloat p = pillFloats[k];
            if (p.rt == null)
                continue;

            Vector2 follow = Vector2.Lerp(NodeFloatOffset(p.fromNode), NodeFloatOffset(p.fromNode + 1), p.along);

            float envelope = Mathf.Sin(p.along * Mathf.PI);
            envelope *= envelope;
            float ownBob = Mathf.Sin(Time.time * pillFloatSpeed + p.phase) * pillFloatAmplitude * envelope;

            p.rt.anchoredPosition = p.basePos + follow + new Vector2(0f, ownBob);
        }
    }

    // How far FloatingIdle has currently pushed a node away from its laid-out position.
    private Vector2 NodeFloatOffset(int nodeIndex)
    {
        if (nodeIndex < 0 || nodeIndex >= spawnedNodeViews.Count || nodeIndex >= nodes.Count)
            return Vector2.zero;

        GameObject view = spawnedNodeViews[nodeIndex];
        if (view == null)
            return Vector2.zero;

        return ((RectTransform)view.transform).anchoredPosition - nodes[nodeIndex].anchoredPos;
    }

    private Sprite[] ResolvePillSprites()
    {
        Sprite[] source = pillSprites;
        if (source == null || source.Length == 0)
        {
            LevelMapPillSet set = Resources.Load<LevelMapPillSet>("LevelMapPills");
            source = set != null ? set.sprites : null;
        }

        List<Sprite> valid = new List<Sprite>();
        if (source != null)
        {
            foreach (Sprite s in source)
                if (s != null)
                    valid.Add(s);
        }
        return valid.ToArray();
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
        pillFloats.Clear();
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
