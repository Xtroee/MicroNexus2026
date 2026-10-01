using UnityEngine;

[CreateAssetMenu(fileName = "BacteriaFrameSet", menuName = "MicroNexus/Bacteria Frame Set")]
public class BacteriaFrameSet : ScriptableObject
{
    public string colorName;
    public Sprite[] frames;
    public float frameRate = 10f;
}
