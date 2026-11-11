using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// "Tarik obat" card (milk bucket). Sits in the HUD like a character card: click it, then click a tile that
/// holds a character to remove that character. Part of the atom price comes back and the card goes on
/// cooldown. The removal itself lives in <see cref="GameManager"/> (it owns tile raycasting and the atoms).
/// Clinical reading: stopping/withdrawing a drug (de-escalation), not "milk neutralises the antibiotic".
/// </summary>
[RequireComponent(typeof(Button))]
public class MilkBucketTool : MonoBehaviour
{
    [Header("Tool")]
    [SerializeField] private float cooldownDuration = 8f;
    [Range(0f, 1f)]
    [SerializeField] private float refundPercent = 0.5f;

    [Header("Card Visuals")]
    [SerializeField] private Image cardImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image cooldownOverlay;
    [SerializeField] private TextMeshProUGUI cooldownText;
    // dark mauve keeps the white/amber labels readable while the tool is armed (brighter values render pastel here)
    [SerializeField] private Color selectedColor = new Color(0.30f, 0.07f, 0.17f, 0.97f);

    private GameManager gms;
    private Button btn;
    private float cooldown;
    private Color originalCardColor = Color.white;

    public float RefundPercent => refundPercent;
    public Sprite Icon => iconImage != null ? iconImage.sprite : null;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.RemoveListener(Toggle);
        btn.onClick.AddListener(Toggle);
        if (cardImage) originalCardColor = cardImage.color;
    }

    private void Start()
    {
        gms = FindFirstObjectByType<GameManager>();
    }

    // The card is switched on for the play phase (Endless) or with the level (normal levels): start ready
    private void OnEnable()
    {
        cooldown = 0f;
        Refresh();
    }

    private void OnDisable()
    {
        if (gms != null && gms.IsRemoveToolActive(this))
            gms.EndRemoveMode();
    }

    private void Update()
    {
        if (cooldown > 0f)
            cooldown -= Time.deltaTime;

        Refresh();
    }

    private void Refresh()
    {
        if (btn == null) return;

        bool active = gms != null && gms.IsRemoveToolActive(this);
        bool blocked = PauseManager.IsPaused || WaveManager.isGameOver;
        bool ready = cooldown <= 0f;

        btn.interactable = !blocked && (ready || active);

        if (cooldownOverlay)
            cooldownOverlay.gameObject.SetActive(!ready);

        if (cooldownText)
        {
            cooldownText.gameObject.SetActive(!ready);
            if (!ready) cooldownText.text = Mathf.CeilToInt(cooldown) + "s";
        }

        if (cardImage)
            cardImage.color = active ? selectedColor : originalCardColor;
    }

    public void Toggle()
    {
        if (gms == null) gms = FindFirstObjectByType<GameManager>();
        if (gms == null) return;
        if (PauseManager.IsPaused || WaveManager.isGameOver) return;

        if (gms.IsRemoveToolActive(this))
        {
            gms.EndRemoveMode();
            return;
        }

        if (cooldown > 0f) return;
        gms.BeginRemoveMode(this);
    }

    /// <summary>Called by GameManager after a character was removed.</summary>
    public void OnUsed()
    {
        cooldown = cooldownDuration;
        Refresh();
    }
}
