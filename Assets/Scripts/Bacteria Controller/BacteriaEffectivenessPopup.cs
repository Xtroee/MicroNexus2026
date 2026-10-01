using UnityEngine;
using TMPro;

public static class BacteriaEffectivenessPopup
{
    private const int EffectiveThreshold = 20;

    public static void Show(Vector3 worldPosition, int damage)
    {
        string label;
        Color color;

        if (damage <= 0)
        {
            label = "Immune";
            color = new Color(0.95f, 0.35f, 0.35f);
        }
        else if (damage < EffectiveThreshold)
        {
            label = "Less Effective";
            color = new Color(1f, 0.85f, 0.3f);
        }
        else
        {
            label = "Effective";
            color = new Color(0.45f, 0.95f, 0.45f);
        }

        GameObject go = BacteriaLabelFactory.Create("EffectivenessPopup", label, color, 2.2f, out TextMeshPro tmp, out SpriteRenderer background);

        // Negative Z = closer to the camera (camera sits at z=-10, transparency
        // sorts by distance along Z) — guarantees this renders above bacteria/VFX
        // regardless of their own sorting layer/order setup.
        go.transform.position = worldPosition + new Vector3(0f, 0.4f, -1f);

        FloatingPopupText floater = go.AddComponent<FloatingPopupText>();
        floater.Init(tmp, background);
    }
}
