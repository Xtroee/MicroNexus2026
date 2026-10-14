using UnityEngine;
using TMPro;

public class FinalResultDisplay : MonoBehaviour
{
    [Header("Final Flags Text")]
    [SerializeField] private TextMeshProUGUI finalFlagText;

    [Header("Final Nucleus Text")]
    [SerializeField] private TextMeshProUGUI finalNucleusText;

    [Header("Final Suns Text")]
    [SerializeField] private TextMeshProUGUI finalSunsText;

    [Header("Final Score Text")]
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [Header("Final Player Name Text")]
    [SerializeField] private TextMeshProUGUI finalPlayerNameText;

    [Header("Leaderboard Rank Text")]
    [SerializeField] private TextMeshProUGUI leaderboardRankText;

    private void Start()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        int flags   = GameData.Data.CurrentFlags;
        int nucleus = GameData.Data.TotalActiveObjects;
        int suns    = GameData.Data.FinalSuns;
        int score   = GameData.Data.FinalScore;
        int rank    = GameData.Data.FinalRank;

        // ✅ Sumber nama utama: UserManager.CurrentUser
        // fallback: GameData.Data.PlayerName
        string playerName =
            (UserManager.Instance != null && !string.IsNullOrEmpty(UserManager.Instance.CurrentUser))
                ? UserManager.Instance.CurrentUser
                : (!string.IsNullOrEmpty(GameData.Data.PlayerName) ? GameData.Data.PlayerName : "Player");

        if (finalFlagText)
            finalFlagText.text = $"WAVE REACHED   {flags}";

        if (finalNucleusText)
            finalNucleusText.text = $"NUCLEUS LEFT   {nucleus}";

        if (finalSunsText)
            finalSunsText.text = $"ENERGY EARNED   {suns}";

        if (finalScoreText)
            finalScoreText.text = $"TOTAL SCORE   {score}";

        if (finalPlayerNameText)
            finalPlayerNameText.text = playerName;

        if (leaderboardRankText)
        {
            // rank biasanya 0-based dari HighscoreTable.Insert()
            // Kalau rank belum pernah di-set (misal -1), jangan bikin pesan "ke-0"
            if (rank < 0)
                leaderboardRankText.text = "Peringkat belum tersedia";
            else if (rank >= 10)
                leaderboardRankText.text = $"Peringkat #{rank + 1}  -  di luar 10 besar";
            else
                leaderboardRankText.text = $"Peringkat #{rank + 1}  -  masuk 10 besar!";
        }
    }
}
