using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Copies the alpha of this object's Image to a CanvasGroup every frame, so child decoration
/// (ring, shadow, labels) fades together with an Image whose alpha is driven by an animation clip
/// (the Death "Kamu Kalah" panel animates only Image.color.a).
/// </summary>
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public class ImageAlphaFollower : MonoBehaviour
{
    private Image image;
    private CanvasGroup group;

    private void Awake()
    {
        image = GetComponent<Image>();
        group = GetComponent<CanvasGroup>();
    }

    private void LateUpdate()
    {
        group.alpha = image.color.a;
    }
}
