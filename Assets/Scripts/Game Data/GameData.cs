using UnityEngine;
using System.Text.RegularExpressions;

public class GameData
{
    public static GameData Data = new GameData();

    // Level progress is stored per player: "UnlockedLevel_<playerName>". The old single shared
    // key is only read once, to migrate existing progress to the first player that loads.
    private const string LEGACY_UNLOCKED_LEVEL_KEY = "UnlockedLevel";
    private const string UNLOCKED_LEVEL_KEY_PREFIX = "UnlockedLevel_";
    private const string LAST_USER_KEY = "LastUser"; // same key UserManager uses

    private string progressUser = "Player";

    // --- Adventure Progression ---
    public int UnlockedLevel = 1;
    public int[] LevelsPerStage = { 12, 1 };

    // Stage index (into LevelsPerStage) that Endless Mode occupies - excluded from the
    // numbered Level Map progression. Mirrors LevelMapController's endlessStageIndex;
    // duplicated here (not read from the controller) so GameData has no dependency on
    // a scene-only MonoBehaviour and can resolve unlocks even before Level Map loads.
    public int EndlessStageIndex = 1;

    public GameData()
    {
        // Restore adventure progress saved by a previous session for the last active player
        // (UserManager re-syncs it on startup and whenever the player is switched). Everything
        // else in this class already defaults sensibly for a fresh run (Endless bests at 0,
        // PlayerName synced separately by UserManager).
        LoadProgressForUser(PlayerPrefs.GetString(LAST_USER_KEY, "Player"));
    }

    private static string ProgressKey(string user)
    {
        return UNLOCKED_LEVEL_KEY_PREFIX + (string.IsNullOrEmpty(user) ? "Player" : user);
    }

    /// <summary>
    /// Loads the saved adventure progress of one player into <see cref="UnlockedLevel"/>. A player
    /// with no saved progress starts at level 1. The first call after updating from the old shared
    /// save moves that progress to the player being loaded, then removes the shared key.
    /// </summary>
    public void LoadProgressForUser(string user)
    {
        progressUser = string.IsNullOrEmpty(user) ? "Player" : user;
        string key = ProgressKey(progressUser);

        if (!PlayerPrefs.HasKey(key) && PlayerPrefs.HasKey(LEGACY_UNLOCKED_LEVEL_KEY))
        {
            PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(LEGACY_UNLOCKED_LEVEL_KEY, 1));
            PlayerPrefs.DeleteKey(LEGACY_UNLOCKED_LEVEL_KEY);
            PlayerPrefs.Save();
            Debug.Log($"[GameData] Migrated shared level progress to player '{progressUser}'");
        }

        UnlockedLevel = PlayerPrefs.GetInt(key, 1);
    }

    /// <summary>Removes the saved progress of a deleted player.</summary>
    public void DeleteProgressForUser(string user)
    {
        PlayerPrefs.DeleteKey(ProgressKey(user));
        PlayerPrefs.Save();
    }

    private static readonly Regex LevelSceneNamePattern = new Regex(@"^Level (\d+) Stage (\d+)$");

    /// <summary>
    /// Call when the player wins a numbered (non-Endless) level. Parses the scene name
    /// ("Level {levelInStage} Stage {stageIndex+1}", matching LevelMapController's own
    /// naming convention), works out that level's position in the overall Level Map
    /// ordinal sequence, and - if it's newly reached - marks it completed and unlocks
    /// the next one, then persists immediately. Safe to call every win; no-ops if this
    /// level was already unlocked/completed, or if sceneName isn't a numbered level
    /// (e.g. Endless Mode).
    /// </summary>
    public void UnlockLevelFromSceneName(string sceneName)
    {
        Match match = LevelSceneNamePattern.Match(sceneName);
        if (!match.Success)
            return;

        int levelInStage = int.Parse(match.Groups[1].Value);
        int stageNumber = int.Parse(match.Groups[2].Value); // 1-based, matches "Stage N" in the name
        int stageIndex = stageNumber - 1;

        if (stageIndex < 0 || stageIndex >= LevelsPerStage.Length || stageIndex == EndlessStageIndex)
            return;

        int globalOrdinal = 0;
        for (int i = 0; i < stageIndex; i++)
        {
            if (i == EndlessStageIndex)
                continue;
            globalOrdinal += LevelsPerStage[i];
        }
        globalOrdinal += levelInStage;

        int newUnlocked = globalOrdinal + 1; // completing ordinal N unlocks/marks-complete up to N, opens N+1
        if (newUnlocked > UnlockedLevel)
        {
            UnlockedLevel = newUnlocked;
            PlayerPrefs.SetInt(ProgressKey(progressUser), UnlockedLevel);
            PlayerPrefs.Save();
            Debug.Log($"[GameData] Level {levelInStage} Stage {stageNumber} completed -> UnlockedLevel={UnlockedLevel} (saved for '{progressUser}')");
        }
    }

    // ============================
    // ENDLESS MODE DATA
    // ============================

    // Last Run
    public int LastCycle = 0;
    public int LastFlags = 0;
    public int LastEndlessScore = 0;

    // Best Run
    public int BestCycle = 0;
    public int BestFlags = 0;
    public int BestEndlessScore = 0;

    public void UpdateEndlessRun(int cycle, int flags)
    {
        int score = cycle * 2 + flags;

        LastCycle = cycle;
        LastFlags = flags;
        LastEndlessScore = score;

        bool improved = false;

        if (cycle > BestCycle) { BestCycle = cycle; improved = true; }
        if (flags > BestFlags) { BestFlags = flags; improved = true; }
        if (score > BestEndlessScore) { BestEndlessScore = score; improved = true; }

        Debug.Log($"[GameData] Endless Run Updated | Last: C={cycle}, F={flags}, S={score} | BestImproved={improved}");
    }

    // ============================
    // SURVIVAL DATA (NEW)
    // ============================
    public int TotalActiveObjects = 0;   // nucleus
    public int FinalSuns = 0;
    public int CurrentFlags = 0;

    // ============================
    // PLAYER NAME (LEADERBOARD)
    // ============================
    public string PlayerName = "";

    // ============================
    // FINAL SCORE & RANK
    // ============================
    public int FinalScore = 0;
    public int FinalRank = -1; // 0-based
}