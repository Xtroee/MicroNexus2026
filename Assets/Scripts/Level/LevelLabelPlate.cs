using TMPro;
using UnityEngine;

/// <summary>
/// Keeps a background plate sized around the level-name label of a Level Map node, so the text
/// stays readable on any part of the map. Put it on the label; the plate is the sibling
/// transform named "Label Plate".
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(TextMeshProUGUI))]
public class LevelLabelPlate : MonoBehaviour
{
    [SerializeField] private float paddingX = 7f;
    [SerializeField] private float paddingY = 4f;

    private TextMeshProUGUI label;
    private RectTransform labelRect;
    private RectTransform plate;

    private void LateUpdate()
    {
        if (label == null) label = GetComponent<TextMeshProUGUI>();
        if (labelRect == null) labelRect = (RectTransform)transform;
        if (plate == null && transform.parent != null)
        {
            Transform p = transform.parent.Find("Label Plate");
            if (p != null) plate = (RectTransform)p;
        }
        if (plate == null) return;

        bool show = label.gameObject.activeInHierarchy && !string.IsNullOrEmpty(label.text);
        if (plate.gameObject.activeSelf != show) plate.gameObject.SetActive(show);
        if (!show) return;

        Vector2 pref = label.GetPreferredValues(label.text, labelRect.rect.width, 0f);
        float w = Mathf.Min(pref.x, labelRect.rect.width) + paddingX * 2f;
        float h = pref.y + paddingY * 2f;

        plate.anchorMin = labelRect.anchorMin;
        plate.anchorMax = labelRect.anchorMax;
        plate.pivot = new Vector2(0.5f, 1f);
        plate.sizeDelta = new Vector2(w, h);
        // the label is top-aligned inside its rect: hang the plate from the rect's top edge
        float topY = labelRect.anchoredPosition.y + (1f - labelRect.pivot.y) * labelRect.rect.height;
        plate.anchoredPosition = new Vector2(labelRect.anchoredPosition.x, topY + paddingY);
    }
}
