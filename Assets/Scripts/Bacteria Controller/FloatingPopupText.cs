using UnityEngine;
using TMPro;

public class FloatingPopupText : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 0.5f;
    [SerializeField] private float lifetime = 1f;

    private TextMeshPro tmp;
    private SpriteRenderer background;
    private float elapsed;
    private Color textStartColor;
    private Color bgStartColor;

    public void Init(TextMeshPro text, SpriteRenderer bg)
    {
        tmp = text;
        background = bg;
        textStartColor = tmp.color;
        bgStartColor = background.color;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        float t = Mathf.Clamp01(elapsed / lifetime);
        float alpha = 1f - t;
        tmp.color = new Color(textStartColor.r, textStartColor.g, textStartColor.b, textStartColor.a * alpha);
        background.color = new Color(bgStartColor.r, bgStartColor.g, bgStartColor.b, bgStartColor.a * alpha);

        if (elapsed >= lifetime)
            Destroy(gameObject);
    }
}
