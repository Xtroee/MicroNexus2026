using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cycles a UI Image's sprite through a frame sequence at a fixed rate, looping.
/// Used for the bacteria animation shown inside a Level Map porthole node.
/// </summary>
public class UISpriteFrameAnimator : MonoBehaviour
{
    [SerializeField] private Image targetImage;

    private Sprite[] frames;
    private float frameRate = 10f;
    private float timer;
    private int frameIndex;

    public void SetFrames(BacteriaFrameSet frameSet)
    {
        if (frameSet == null || frameSet.frames == null || frameSet.frames.Length == 0)
        {
            enabled = false;
            return;
        }

        frames = frameSet.frames;
        frameRate = frameSet.frameRate > 0f ? frameSet.frameRate : 10f;
        timer = 0f;
        frameIndex = 0;

        if (targetImage != null)
            targetImage.sprite = frames[0];

        enabled = true;
    }

    private void Update()
    {
        if (targetImage == null || frames == null || frames.Length == 0)
            return;

        timer += Time.unscaledDeltaTime;
        float frameDuration = 1f / frameRate;

        while (timer >= frameDuration)
        {
            timer -= frameDuration;
            frameIndex = (frameIndex + 1) % frames.Length;
            targetImage.sprite = frames[frameIndex];
        }
    }
}
