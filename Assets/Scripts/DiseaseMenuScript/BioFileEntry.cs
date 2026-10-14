using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Bridges the "BIO-FILE (RANGER)" / "BIO-FILE (ENEMY)" buttons on the Halaman
/// Belajar (disease menu) screen to the RangerGroup prefab instance, and gives it
/// a way back out. RangerGroup's own internal navigation (QuestionMarkManager) only
/// knows how to move between its own sub-panels - closing the whole thing and
/// returning to the disease menu is scene-specific, so it lives here instead.
/// </summary>
public class BioFileEntry : MonoBehaviour
{
    [SerializeField] private GameObject bioFileRoot;
    [SerializeField] private QuestionMarkManager questionMarkManager;

    private Coroutine pendingOpen;

    public void OpenRanger()
    {
        Open(() => questionMarkManager?.ShowRangerRoot());
    }

    public void OpenEnemy()
    {
        Open(() => questionMarkManager?.ShowEnemyBioChoose());
    }

    /// <summary>
    /// Activating the RangerGroup root runs the Start()/OnEnable() of its managers on the next frame,
    /// and QuestionMarkManager's Start resets everything to its default (hidden) state. Showing the
    /// panel in the same frame as the activation therefore got undone and the button needed a second
    /// press. When the root was inactive we wait one frame so our call lands after that reset.
    /// </summary>
    private void Open(Action show)
    {
        if (bioFileRoot == null)
        {
            show();
            return;
        }

        if (pendingOpen != null)
        {
            StopCoroutine(pendingOpen);
            pendingOpen = null;
        }

        if (bioFileRoot.activeSelf)
        {
            show();
            return;
        }

        bioFileRoot.SetActive(true);
        pendingOpen = StartCoroutine(ShowAfterInit(show));
    }

    private IEnumerator ShowAfterInit(Action show)
    {
        yield return null;          // managers' Start()/OnEnable() have run by now
        pendingOpen = null;
        if (bioFileRoot != null && bioFileRoot.activeSelf)
            show();
    }

    public void Close()
    {
        if (pendingOpen != null)
        {
            StopCoroutine(pendingOpen);
            pendingOpen = null;
        }
        if (bioFileRoot != null) bioFileRoot.SetActive(false);
        Time.timeScale = 1f; // QuestionMarkManager.ShowQuestionMark() pauses time; undo that on the way out
    }
}
