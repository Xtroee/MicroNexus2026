using UnityEngine;
using UnityEngine.Serialization;

public class BioManager : MonoBehaviour
{
    [Header("Semua Panel / GameObject (Optional)")]
    [SerializeField] private GameObject stageSelectionGO;
    [SerializeField] private GameObject bioRangerGO;
    [SerializeField] private GameObject bioEnemyGO;
    [SerializeField] private GameObject howToPlayGO;

    // =====================
    // ENEMY BIO SUB PANELS
    // =====================
    [Header("Enemy Bio Panels")]
    [SerializeField] private GameObject ChooseEnemyBioPanel;
    [SerializeField] private GameObject EnemyOtherBioPanel;
    [SerializeField] private GameObject EnemyBacteriaBioPanel;

    // =====================
    // RANGER CHARACTERS (Optional)
    // =====================
    [Header("Ranger Characters (Optional)")]
    [FormerlySerializedAs("whiteCharacter")] [SerializeField] private GameObject amoxicillinCharacter;
    [FormerlySerializedAs("blueCharacter")] [SerializeField] private GameObject vancomisinCharacter;
    [FormerlySerializedAs("pinkCharacter")] [SerializeField] private GameObject cefiximeCharacter;
    [FormerlySerializedAs("redCharacter")] [SerializeField] private GameObject azitromisinCharacter;
    [FormerlySerializedAs("greenCharacter")] [SerializeField] private GameObject ciprofloxacinCharacter;
    [FormerlySerializedAs("yellowCharacter")] [SerializeField] private GameObject levofloksasinCharacter;
    [FormerlySerializedAs("blackCharacter")] [SerializeField] private GameObject cotrimoxazoleCharacter;

    // =====================
    // BACTERIA ENEMIES
    // =====================
    [Header("Bacteria Enemies (Optional)")]
    [SerializeField] private GameObject bacteriaPurple;
    [SerializeField] private GameObject bacteriaRed;
    [SerializeField] private GameObject bacteriaYellow;
    [SerializeField] private GameObject bacteriaOrange;
    [SerializeField] private GameObject bacteriaPink;
    [SerializeField] private GameObject bacteriaBlue;
    [SerializeField] private GameObject bacteriaGreen;

    // =====================
    // OTHER ENEMIES
    // =====================
    [Header("Other Enemies (Optional)")]
    [SerializeField] private GameObject fungiEnemy;
    [SerializeField] private GameObject virusEnemy;
    [SerializeField] private GameObject protozoaEnemy;
    [SerializeField] private GameObject helminthEnemy;

    private void Start()
    {
        ShowStageSelection();
    }

    // =====================
    // GAMEOBJECT / PANEL BEHAVIOUR
    // =====================
    public void ShowStageSelection()
    {
        if (stageSelectionGO != null) stageSelectionGO.SetActive(true);
        if (bioRangerGO != null) bioRangerGO.SetActive(false);
        if (bioEnemyGO != null) bioEnemyGO.SetActive(false);
        if (howToPlayGO != null) howToPlayGO.SetActive(false);

        DisableAllRangers();
        DisableAllEnemies();
        DisableAllEnemyBioPanels();
    }

    public void ShowBioRanger()
    {
        if (stageSelectionGO != null) stageSelectionGO.SetActive(false);
        if (bioRangerGO != null) bioRangerGO.SetActive(true);
        if (bioEnemyGO != null) bioEnemyGO.SetActive(false);
        if (howToPlayGO != null) howToPlayGO.SetActive(false);

        DisableAllRangers();
        DisableAllEnemies();
        DisableAllEnemyBioPanels();
    }

    public void ShowBioEnemy()
    {
        if (stageSelectionGO != null) stageSelectionGO.SetActive(false);
        if (bioRangerGO != null) bioRangerGO.SetActive(false);
        if (bioEnemyGO != null) bioEnemyGO.SetActive(true);
        if (howToPlayGO != null) howToPlayGO.SetActive(false);

        DisableAllRangers();
        DisableAllEnemies();
        DisableAllEnemyBioPanels();
    }

    public void ShowHowToPlay()
    {
        if (stageSelectionGO != null) stageSelectionGO.SetActive(false);
        if (bioRangerGO != null) bioRangerGO.SetActive(false);
        if (bioEnemyGO != null) bioEnemyGO.SetActive(false);
        if (howToPlayGO != null) howToPlayGO.SetActive(true);

        DisableAllRangers();
        DisableAllEnemies();
        DisableAllEnemyBioPanels();
    }

    // =====================
    // RANGER BEHAVIOUR
    // =====================
    private void DisableAllRangers()
    {
        if (amoxicillinCharacter != null) amoxicillinCharacter.SetActive(false);
        if (vancomisinCharacter != null) vancomisinCharacter.SetActive(false);
        if (cefiximeCharacter != null) cefiximeCharacter.SetActive(false);
        if (azitromisinCharacter != null) azitromisinCharacter.SetActive(false);
        if (ciprofloxacinCharacter != null) ciprofloxacinCharacter.SetActive(false);
        if (levofloksasinCharacter != null) levofloksasinCharacter.SetActive(false);
        if (cotrimoxazoleCharacter != null) cotrimoxazoleCharacter.SetActive(false);
    }

    private void ShowOnlyRanger(GameObject target)
    {
        if (amoxicillinCharacter != null) amoxicillinCharacter.SetActive(target == amoxicillinCharacter);
        if (vancomisinCharacter != null) vancomisinCharacter.SetActive(target == vancomisinCharacter);
        if (cefiximeCharacter != null) cefiximeCharacter.SetActive(target == cefiximeCharacter);
        if (azitromisinCharacter != null) azitromisinCharacter.SetActive(target == azitromisinCharacter);
        if (ciprofloxacinCharacter != null) ciprofloxacinCharacter.SetActive(target == ciprofloxacinCharacter);
        if (levofloksasinCharacter != null) levofloksasinCharacter.SetActive(target == levofloksasinCharacter);
        if (cotrimoxazoleCharacter != null) cotrimoxazoleCharacter.SetActive(target == cotrimoxazoleCharacter);
    }

    public void ShowAmoxicillin()   => ShowOnlyRanger(amoxicillinCharacter);
    public void ShowVancomisin()    => ShowOnlyRanger(vancomisinCharacter);
    public void ShowCefixime()      => ShowOnlyRanger(cefiximeCharacter);
    public void ShowAzitromisin()   => ShowOnlyRanger(azitromisinCharacter);
    public void ShowCiprofloxacin() => ShowOnlyRanger(ciprofloxacinCharacter);
    public void ShowLevofloksasin() => ShowOnlyRanger(levofloksasinCharacter);
    public void ShowCotrimoxazole() => ShowOnlyRanger(cotrimoxazoleCharacter);

    // =====================
    // ENEMY BEHAVIOUR
    // =====================
    private void DisableAllEnemies()
    {
        if (bacteriaPurple != null) bacteriaPurple.SetActive(false);
        if (bacteriaRed != null) bacteriaRed.SetActive(false);
        if (bacteriaYellow != null) bacteriaYellow.SetActive(false);
        if (bacteriaOrange != null) bacteriaOrange.SetActive(false);
        if (bacteriaPink != null) bacteriaPink.SetActive(false);
        if (bacteriaBlue != null) bacteriaBlue.SetActive(false);
        if (bacteriaGreen != null) bacteriaGreen.SetActive(false);

        if (fungiEnemy != null) fungiEnemy.SetActive(false);
        if (virusEnemy != null) virusEnemy.SetActive(false);
        if (protozoaEnemy != null) protozoaEnemy.SetActive(false);
        if (helminthEnemy != null) helminthEnemy.SetActive(false);
    }

    // =====================
    // ENEMY BIO PANEL BEHAVIOUR
    // =====================
    private void DisableAllEnemyBioPanels()
    {
        if (ChooseEnemyBioPanel != null) ChooseEnemyBioPanel.SetActive(false);
        if (EnemyOtherBioPanel != null) EnemyOtherBioPanel.SetActive(false);
        if (EnemyBacteriaBioPanel != null) EnemyBacteriaBioPanel.SetActive(false);
    }

    public void ShowChooseEnemyBioPanel()
    {
        DisableAllEnemyBioPanels();
        DisableAllEnemies();
        if (ChooseEnemyBioPanel != null) ChooseEnemyBioPanel.SetActive(true);
    }

    public void ShowEnemyOtherBioPanel()
    {
        DisableAllEnemyBioPanels();
        DisableAllEnemies();
        if (EnemyOtherBioPanel != null) EnemyOtherBioPanel.SetActive(true);
    }

    public void ShowEnemyBacteriaBioPanel()
    {
        DisableAllEnemyBioPanels();
        DisableAllEnemies();
        if (EnemyBacteriaBioPanel != null) EnemyBacteriaBioPanel.SetActive(true);
    }
}
