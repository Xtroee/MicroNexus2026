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

    public void OpenRanger()
    {
        if (bioFileRoot != null) bioFileRoot.SetActive(true);
        questionMarkManager?.ShowRangerRoot();
    }

    public void OpenEnemy()
    {
        if (bioFileRoot != null) bioFileRoot.SetActive(true);
        questionMarkManager?.ShowEnemyBioChoose();
    }

    public void Close()
    {
        if (bioFileRoot != null) bioFileRoot.SetActive(false);
        Time.timeScale = 1f; // QuestionMarkManager.ShowQuestionMark() pauses time; undo that on the way out
    }
}
