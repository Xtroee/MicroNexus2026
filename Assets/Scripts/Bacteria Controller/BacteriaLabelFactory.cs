using UnityEngine;
using TMPro;

public static class BacteriaLabelFactory
{
    private static Sprite backgroundSprite;

    private static Sprite GetBackgroundSprite()
    {
        if (backgroundSprite == null)
        {
            var tex = new Texture2D(4, 4);
            var pixels = new Color[16];
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = Color.white;
            tex.SetPixels(pixels);
            tex.Apply();
            backgroundSprite = Sprite.Create(tex, new Rect(0, 0, 4, 4), new Vector2(0.5f, 0.5f), 4f);
        }

        return backgroundSprite;
    }

    // Builds a text label with a solid dark backing panel sized to fit the text,
    // so it stays readable regardless of whatever's behind it in the game world.
    public static GameObject Create(string name, string text, Color textColor, float fontSize, out TextMeshPro tmp, out SpriteRenderer background)
    {
        GameObject root = new GameObject(name);

        GameObject bgGo = new GameObject(name + "_BG");
        bgGo.transform.SetParent(root.transform);
        bgGo.transform.localPosition = new Vector3(0f, 0f, 0.15f);
        background = bgGo.AddComponent<SpriteRenderer>();
        background.sprite = GetBackgroundSprite();
        background.color = new Color(0f, 0f, 0f, 0.75f);
        background.sortingOrder = 499;

        GameObject textGo = new GameObject(name + "_Text");
        textGo.transform.SetParent(root.transform);
        textGo.transform.localPosition = Vector3.zero;
        tmp = textGo.AddComponent<TextMeshPro>();
        tmp.text = text;
        tmp.color = textColor;
        tmp.fontSize = fontSize;
        tmp.fontStyle = FontStyles.Bold;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.sortingOrder = 500;

        tmp.ForceMeshUpdate();
        Vector3 size = tmp.textBounds.size;
        bgGo.transform.localScale = new Vector3(Mathf.Max(size.x + 0.3f, 0.6f), size.y + 0.18f, 1f);

        return root;
    }
}
