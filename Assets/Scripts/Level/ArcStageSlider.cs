using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlideObjectLeft : MonoBehaviour
{
    [Header("Object Settings")]
    public RectTransform objectToSlide; // Object yang mau digeser (harus ada RectTransform)
    public float slideDuration = 1f; // Waktu animasi dalam detik
    public float targetX = -1920f; // Posisi X target (kiri)
    public float startX = 0f; // Posisi X awal

    private bool isSliding = false;
    private Vector2 startPos;
    private Vector2 targetPos;

    void Start()
    {
        // Kalo object belum di-assign di Inspector, coba ambil dari gameobject ini
        if (objectToSlide == null)
            objectToSlide = GetComponent<RectTransform>();

        // Set posisi awal
        if (objectToSlide != null)
        {
            startPos = new Vector2(startX, objectToSlide.anchoredPosition.y);
            targetPos = new Vector2(targetX, objectToSlide.anchoredPosition.y);
            objectToSlide.anchoredPosition = startPos;
        }
    }

    // Fungsi ini bakal dipanggil dari Button
    public void SlideLeft()
    {
        if (!isSliding && objectToSlide != null)
        {
            StartCoroutine(SlideAnimation());
        }
    }

    IEnumerator SlideAnimation()
    {
        isSliding = true;
        float elapsedTime = 0f;

        Vector2 currentPos = objectToSlide.anchoredPosition;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / slideDuration;

            // Pake smooth step biar gerakannya halus
            float smoothT = t * t * (3f - 2f * t);

            objectToSlide.anchoredPosition = Vector2.Lerp(startPos, targetPos, smoothT);

            yield return null; // Tunggu frame selanjutnya
        }

        // Pastikan posisi akhir tepat
        objectToSlide.anchoredPosition = targetPos;
        isSliding = false;
    }

    // Opsional: Buat reset ke posisi awal
    public void ResetPosition()
    {
        if (objectToSlide != null)
        {
            StopAllCoroutines();
            objectToSlide.anchoredPosition = startPos;
            isSliding = false;
        }
    }
}