using UnityEngine;
using TMPro;

/// <summary>
/// Floating "-N HP" feedback shown when a placed antibiotic takes damage from a bacteria's
/// melee attack. Mirrors BacteriaEffectivenessPopup's look/animation (same factory + floater).
/// </summary>
public static class DefenderHitPopup
{
    private static readonly Color DamageColor = new Color(0.95f, 0.35f, 0.35f);

    public static void Show(Vector3 worldPosition, int damage)
    {
        GameObject go = BacteriaLabelFactory.Create("DefenderHitPopup", $"-{damage} HP", DamageColor, 2.2f, out TextMeshPro tmp, out SpriteRenderer background);

        go.transform.position = worldPosition + new Vector3(0f, 0.4f, -1f);

        FloatingPopupText floater = go.AddComponent<FloatingPopupText>();
        floater.Init(tmp, background);
    }
}
