using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Resets a ScrollRect to the top whenever it is shown. The Bio-File lists grew (14 rangers / 14 bacteria)
/// and the content is taller than the viewport, so without this the list opens scrolled to the middle.
/// The reset runs one frame later, after the layout groups have measured the content.
/// </summary>
[RequireComponent(typeof(ScrollRect))]
public class ScrollToTopOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(ResetNextFrame());
    }

    private IEnumerator ResetNextFrame()
    {
        yield return null;
        ScrollRect rect = GetComponent<ScrollRect>();
        Canvas.ForceUpdateCanvases();
        rect.verticalNormalizedPosition = 1f;
    }
}
