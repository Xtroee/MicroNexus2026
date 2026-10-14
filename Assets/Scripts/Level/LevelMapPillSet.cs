using UnityEngine;

/// <summary>
/// Sprites scattered along the Level Map path in place of the plain connector dots.
/// LevelMapController loads the asset at Resources/LevelMapPills when its own
/// pillSprites array is left empty.
/// </summary>
[CreateAssetMenu(fileName = "LevelMapPills", menuName = "MicroNexus/Level Map Pill Set")]
public class LevelMapPillSet : ScriptableObject
{
    public Sprite[] sprites;
}
