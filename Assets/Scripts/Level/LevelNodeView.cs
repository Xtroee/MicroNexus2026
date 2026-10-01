using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum NodeState
{
    Locked,
    Unlocked,
    Completed
}

public class LevelNodeView : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI levelNumberLabel;
    [SerializeField] private TextMeshProUGUI levelNameLabel;

    [Header("State Visuals")]
    [SerializeField] private Image imageBase;
    [SerializeField] private Image dimOverlay;
    [SerializeField] private GameObject lockOverlay;
    [SerializeField] private GameObject completedBadge;

    [Header("Porthole Bacteria Animation")]
    [SerializeField] private UISpriteFrameAnimator bacteriaAnimator;
    [SerializeField] private GameObject bacteriaAnimRoot;

    [Header("State Sprites (Image_Base)")]
    [SerializeField] private Sprite baseSpriteLocked;
    [SerializeField] private Sprite baseSpriteUnlocked;
    [SerializeField] private Sprite baseSpriteCompleted;

    [Header("State Tint (marks state while sprites are still placeholders)")]
    [SerializeField] private Color tintLocked = new Color(0.6f, 0.6f, 0.6f, 1f);
    [SerializeField] private Color tintUnlocked = Color.white;

    private LevelNodeData data;
    private Action<LevelNodeData> onClick;

    public void Setup(LevelNodeData nodeData, Action<LevelNodeData> onClickCallback)
    {
        data = nodeData;
        onClick = onClickCallback;

        if (levelNumberLabel != null)
            levelNumberLabel.text = data.levelInStage.ToString();

        if (levelNameLabel != null)
            levelNameLabel.text = data.displayName;

        SetState(data.state);
        SetBacteria(nodeData.bacteriaFrameSet);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(HandleClick);
    }

    public void SetBacteria(BacteriaFrameSet frameSet)
    {
        if (bacteriaAnimator != null)
            bacteriaAnimator.SetFrames(frameSet);

        if (bacteriaAnimRoot != null)
            bacteriaAnimRoot.SetActive(frameSet != null && data.state != NodeState.Locked);
    }

    public void SetState(NodeState state)
    {
        data.state = state;

        button.interactable = state != NodeState.Locked;

        if (dimOverlay != null)
            dimOverlay.gameObject.SetActive(state == NodeState.Locked);

        if (lockOverlay != null)
            lockOverlay.SetActive(state == NodeState.Locked);

        if (completedBadge != null)
            completedBadge.SetActive(state == NodeState.Completed);

        if (levelNumberLabel != null)
            levelNumberLabel.gameObject.SetActive(state != NodeState.Locked);

        if (levelNameLabel != null)
            levelNameLabel.gameObject.SetActive(state != NodeState.Locked);

        if (bacteriaAnimRoot != null)
            bacteriaAnimRoot.SetActive(state != NodeState.Locked && bacteriaAnimator != null);

        if (imageBase != null)
        {
            imageBase.sprite = state switch
            {
                NodeState.Locked => baseSpriteLocked,
                NodeState.Completed => baseSpriteCompleted,
                _ => baseSpriteUnlocked
            };

            imageBase.color = state == NodeState.Locked ? tintLocked : tintUnlocked;
        }
    }

    private void HandleClick()
    {
        onClick?.Invoke(data);
    }
}
