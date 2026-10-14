using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Hover / press polish for the Mode Selection cards: the card scales up, the gameplay
/// screenshot zooms in and brightens, the frame glows and the shadow deepens.
/// Runs on unscaled time so it still animates while Time.timeScale is 0.
/// </summary>
public class ModeCardHover : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler,
    ISelectHandler, IDeselectHandler
{
    [Header("Targets")]
    [SerializeField] private RectTransform card;
    [SerializeField] private RectTransform screenshot;
    [SerializeField] private Image screenshotImage;
    [SerializeField] private Image frame;
    [SerializeField] private Image shadow;

    [Header("Motion")]
    [SerializeField] private float hoverScale = 1.05f;
    [SerializeField] private float pressScale = 0.97f;
    [SerializeField] private float screenshotZoom = 1.08f;
    [SerializeField] private float speed = 12f;

    [Header("Colours")]
    [SerializeField] private Color idleScreenshotTint = new Color(0.78f, 0.8f, 0.88f, 1f);
    [SerializeField] private float frameGlow = 0.4f;
    [SerializeField] private float shadowIdleAlpha = 0.5f;
    [SerializeField] private float shadowHoverAlpha = 0.8f;

    private bool hovered;
    private bool pressed;
    private Color frameIdle;
    private Color frameHover;

    private void Awake()
    {
        if (card == null)
            card = (RectTransform)transform;

        if (frame != null)
        {
            frameIdle = frame.color;
            frameHover = Color.Lerp(frameIdle, Color.white, frameGlow);
        }

        Apply(0f, true);
    }

    private void OnDisable()
    {
        hovered = false;
        pressed = false;
        Apply(0f, true);
    }

    private void Update()
    {
        Apply(speed * Time.unscaledDeltaTime, false);
    }

    private void Apply(float step, bool snap)
    {
        float cardTarget = pressed ? pressScale : (hovered ? hoverScale : 1f);
        float zoomTarget = hovered ? screenshotZoom : 1f;
        float k = snap ? 1f : Mathf.Clamp01(step);

        if (card != null)
        {
            float s = Mathf.Lerp(card.localScale.x, cardTarget, k);
            card.localScale = new Vector3(s, s, 1f);
        }

        if (screenshot != null)
        {
            float z = Mathf.Lerp(screenshot.localScale.x, zoomTarget, k);
            screenshot.localScale = new Vector3(z, z, 1f);
        }

        if (screenshotImage != null)
            screenshotImage.color = Color.Lerp(screenshotImage.color, hovered ? Color.white : idleScreenshotTint, k);

        if (frame != null)
            frame.color = Color.Lerp(frame.color, hovered ? frameHover : frameIdle, k);

        if (shadow != null)
        {
            Color c = shadow.color;
            c.a = Mathf.Lerp(c.a, hovered ? shadowHoverAlpha : shadowIdleAlpha, k);
            shadow.color = c;
        }
    }

    public void OnPointerEnter(PointerEventData e) { hovered = true; }
    public void OnPointerExit(PointerEventData e) { hovered = false; pressed = false; }
    public void OnPointerDown(PointerEventData e) { pressed = true; }
    public void OnPointerUp(PointerEventData e) { pressed = false; }
    public void OnSelect(BaseEventData e) { hovered = true; }
    public void OnDeselect(BaseEventData e) { hovered = false; }
}
