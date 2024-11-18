using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogManager : MonoBehaviour
{
    public Tilemap blackSpriteRenderer;
    public float fadeDuration = 1.0f; // Kararma efektinin süresi

    private bool isFading = false;
    private float currentFadeTime = 0.0f;
    private Color originalColor;

    void Start()
    {
        // Baþlangýçta sprite'ýn orijinal rengini kaydet
        originalColor = blackSpriteRenderer.color;
    }

    void FixedUpdate()
    {
        if (isFading)
        {
            currentFadeTime += Time.deltaTime;

            float t = Mathf.Clamp01(currentFadeTime / fadeDuration);

            // Karartma efektini kademeli olarak uygula
            blackSpriteRenderer.color = new Color(0, 0, 0, t);

            if (currentFadeTime >= fadeDuration)
            {
                isFading = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KoridorBlack") && !isFading)
        {
            StartCoroutine(FadeIn());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("KoridorBlack") && !isFading)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        isFading = true;
        currentFadeTime = 0.0f;

        while (isFading)
        {
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        isFading = true;
        currentFadeTime = 0.0f;

        while (currentFadeTime < fadeDuration)
        {
            currentFadeTime += Time.deltaTime;
            float t = Mathf.Clamp01(currentFadeTime / fadeDuration);

            // Karartma efektini kademeli olarak geri çevir
            blackSpriteRenderer.color = Color.Lerp(new Color(0, 0, 0, 1), originalColor, t);

            yield return null;
        }

        // Karartma efektini sýfýrla
        blackSpriteRenderer.color = originalColor;
        isFading = false;
    }
}
