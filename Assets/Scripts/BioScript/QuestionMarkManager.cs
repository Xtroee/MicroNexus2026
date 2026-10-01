using UnityEngine;

public class QuestionMarkManager : MonoBehaviour
{
    // ========================
    // CANVAS
    // ========================
    [Header("Canvas")]
    [SerializeField] private GameObject canvasDefault;
    [SerializeField] private GameObject canvasQuestionMark;

    // ========================
    // RANGER
    // ========================
    [Header("Ranger Canvas Root")]
    [SerializeField] private GameObject rangerCanvasRoot;

    [Header("Ranger Canvas")]
    [SerializeField] private GameObject rangerWhiteCanvas;
    [SerializeField] private GameObject rangerBlueCanvas;
    [SerializeField] private GameObject rangerPinkCanvas;
    [SerializeField] private GameObject rangerRedCanvas;
    [SerializeField] private GameObject rangerGreenCanvas;
    [SerializeField] private GameObject rangerYellowCanvas;
    [SerializeField] private GameObject rangerBlackCanvas;

    // ========================
    // ENEMY BIO MODE
    // ========================
    [Header("Enemy Bio Manager")]
    [SerializeField] private GameObject EnemyBioChoose;
    [SerializeField] private GameObject enemyBacteriaBioManager;
    [SerializeField] private GameObject enemyOtherBioManager;

    // ========================
    // ENEMY DETAIL PANELS
    // ========================
    [Header("Enemy Panels - Bacteria")]
    [SerializeField] private GameObject enemyPurplePanel;
    [SerializeField] private GameObject enemyRedPanel;
    [SerializeField] private GameObject enemyBluePanel;
    [SerializeField] private GameObject enemyPinkPanel;
    [SerializeField] private GameObject enemyOrangePanel;
    [SerializeField] private GameObject enemyYellowPanel;
    [SerializeField] private GameObject enemyGreenPanel;

    [Header("Enemy Panels - Other")]
    [SerializeField] private GameObject enemyVirusPanel;
    [SerializeField] private GameObject enemyFungiPanel;
    [SerializeField] private GameObject enemyProtozoaPanel;
    [SerializeField] private GameObject enemyHelminthPanel;

    // ========================
    // BLOCKER
    // ========================
    [Header("Blocker (Optional)")]
    [SerializeField] private GameObject blockCanvas;

    private void Start()
    {
        ShowDefault();
    }

    // Several fields on this component are legitimately left unassigned depending on
    // which context the prefab is dropped into (e.g. canvasDefault). Unity's `?.`
    // operator does NOT reliably short-circuit on an unassigned serialized
    // UnityEngine.Object reference - it can still throw UnassignedReferenceException
    // deep inside SetActive(). Route every toggle through this explicit null check
    // instead of `field?.SetActive(...)`.
    private static void SafeSetActive(GameObject go, bool active)
    {
        if (go != null)
            go.SetActive(active);
    }

    // ========================
    // DEFAULT
    // ========================
    public void ShowDefault()
    {
        if (IsBlocked())
        {
            Debug.Log("[QuestionMarkManager] ShowDefault blocked because '"
                + (blockCanvas != null ? blockCanvas.name : "NULL") + "' is active.");
            return;
        }

        DisableAllContent();

        SafeSetActive(canvasDefault, true);
        SafeSetActive(canvasQuestionMark, false);

        Time.timeScale = 1f;
        if (SoundManager.Instance != null) SoundManager.Instance.ResumeBgm();
    }

    // ========================
    // QUESTION MARK ONLY
    // ========================
    public void ShowQuestionMark()
    {
        if (IsBlocked())
        {
            Debug.Log("[QuestionMarkManager] ShowQuestionMark blocked because '"
                + (blockCanvas != null ? blockCanvas.name : "NULL") + "' is active.");
            return;
        }

        DisableAllContent();

        SafeSetActive(canvasDefault, false);
        SafeSetActive(canvasQuestionMark, true);

        Time.timeScale = 0f;
        if (SoundManager.Instance != null) SoundManager.Instance.PauseBgm();
    }

    // ========================
    // ENEMY BIO CHOOSE
    // ========================
    public void ShowEnemyBioChoose()
    {
        if (IsBlocked())
        {
            Debug.Log("[QuestionMarkManager] ShowEnemyBioChoose blocked because '"
                + (blockCanvas != null ? blockCanvas.name : "NULL") + "' is active.");
            return;
        }

        DisableAllContent();

        SafeSetActive(canvasQuestionMark, true);
        SafeSetActive(EnemyBioChoose, true);
    }

    // ========================
    // BIO MODE
    // ========================
    public void ShowEnemyBacteriaBioManager()
    {
        if (IsBlocked())
        {
            Debug.Log("[QuestionMarkManager] ShowEnemyBacteriaBioManager blocked because '"
                + (blockCanvas != null ? blockCanvas.name : "NULL") + "' is active.");
            return;
        }

        DisableAllContent();

        SafeSetActive(canvasQuestionMark, true);
        SafeSetActive(EnemyBioChoose, true);
        SafeSetActive(enemyBacteriaBioManager, true);
    }

    public void ShowEnemyOtherBioManager()
    {
        if (IsBlocked())
        {
            Debug.Log("[QuestionMarkManager] ShowEnemyOtherBioManager blocked because '"
                + (blockCanvas != null ? blockCanvas.name : "NULL") + "' is active.");
            return;
        }

        DisableAllContent();

        SafeSetActive(canvasQuestionMark, true);
        SafeSetActive(EnemyBioChoose, true);
        SafeSetActive(enemyOtherBioManager, true);
    }

    // ========================
    // ENEMY DETAIL
    // ========================
    public void ShowEnemy(GameObject enemyPanel)
    {
        if (IsBlocked())
        {
            Debug.Log("[QuestionMarkManager] ShowEnemy blocked because '"
                + (blockCanvas != null ? blockCanvas.name : "NULL") + "' is active.");
            return;
        }

        DisableAllContent();

        SafeSetActive(canvasQuestionMark, true);
        SafeSetActive(enemyPanel, true);
    }

    public void ShowEnemyPurple()   => ShowEnemy(enemyPurplePanel);
    public void ShowEnemyRed()      => ShowEnemy(enemyRedPanel);
    public void ShowEnemyBlue()     => ShowEnemy(enemyBluePanel);
    public void ShowEnemyPink()     => ShowEnemy(enemyPinkPanel);
    public void ShowEnemyOrange()   => ShowEnemy(enemyOrangePanel);
    public void ShowEnemyYellow()   => ShowEnemy(enemyYellowPanel);
    public void ShowEnemyGreen()    => ShowEnemy(enemyGreenPanel);
    public void ShowEnemyVirus()    => ShowEnemy(enemyVirusPanel);
    public void ShowEnemyFungi()    => ShowEnemy(enemyFungiPanel);
    public void ShowEnemyProtozoa() => ShowEnemy(enemyProtozoaPanel);
    public void ShowEnemyHelminth() => ShowEnemy(enemyHelminthPanel);

    // ========================
    // RANGER
    // ========================
    public void ShowRanger(GameObject ranger)
    {
        if (IsBlocked())
        {
            Debug.Log("[QuestionMarkManager] ShowRanger blocked because '"
                + (blockCanvas != null ? blockCanvas.name : "NULL") + "' is active.");
            return;
        }

        DisableAllContent();

        SafeSetActive(canvasQuestionMark, false);
        SafeSetActive(canvasDefault, false);

        SafeSetActive(ranger, true);
    }

    public void ShowRangerRoot()   => ShowRanger(rangerCanvasRoot);
    public void ShowRangerWhite()  => ShowRanger(rangerWhiteCanvas);
    public void ShowRangerBlue()   => ShowRanger(rangerBlueCanvas);
    public void ShowRangerPink()   => ShowRanger(rangerPinkCanvas);
    public void ShowRangerRed()    => ShowRanger(rangerRedCanvas);
    public void ShowRangerGreen()  => ShowRanger(rangerGreenCanvas);
    public void ShowRangerYellow() => ShowRanger(rangerYellowCanvas);
    public void ShowRangerBlack()  => ShowRanger(rangerBlackCanvas);

    // ========================
    // DISABLE CONTENT ONLY
    // ========================
    private void DisableAllContent()
    {
        SafeSetActive(EnemyBioChoose, false);
        SafeSetActive(enemyBacteriaBioManager, false);
        SafeSetActive(enemyOtherBioManager, false);

        SafeSetActive(enemyPurplePanel, false);
        SafeSetActive(enemyRedPanel, false);
        SafeSetActive(enemyBluePanel, false);
        SafeSetActive(enemyPinkPanel, false);
        SafeSetActive(enemyOrangePanel, false);
        SafeSetActive(enemyYellowPanel, false);
        SafeSetActive(enemyGreenPanel, false);
        SafeSetActive(enemyVirusPanel, false);
        SafeSetActive(enemyFungiPanel, false);
        SafeSetActive(enemyProtozoaPanel, false);
        SafeSetActive(enemyHelminthPanel, false);

        SafeSetActive(rangerCanvasRoot, false);
        SafeSetActive(rangerWhiteCanvas, false);
        SafeSetActive(rangerBlueCanvas, false);
        SafeSetActive(rangerPinkCanvas, false);
        SafeSetActive(rangerRedCanvas, false);
        SafeSetActive(rangerGreenCanvas, false);
        SafeSetActive(rangerYellowCanvas, false);
        SafeSetActive(rangerBlackCanvas, false);
    }

    private bool IsBlocked()
    {
        bool blocked = blockCanvas != null && blockCanvas.activeSelf;

        if (blocked)
        {
            Debug.Log("[QuestionMarkManager] BLOCKED by GameObject: '"
                + blockCanvas.name + "' (activeSelf = TRUE)");
        }

        return blocked;
    }
}
