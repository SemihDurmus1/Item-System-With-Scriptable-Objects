using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadeManager : MonoBehaviour
{
    private Tilemap blackMap;

    public float fadeDuration = 1.0f; // Kararma efektinin süresi

    private bool isFading = false;

    private float currentFadeTime = 0.0f;

    void Start()
    {
        blackMap = GetComponent<Tilemap>();

    }

    void FixedUpdate()
    {
        if (isFading)
        {
            currentFadeTime += Time.deltaTime;

            float t = Mathf.Clamp01(currentFadeTime / fadeDuration);

            // Karartma efektini kademeli olarak uygula
            blackMap.color = new Color(0, 0, 0, t);

            if (currentFadeTime >= fadeDuration)
            {
                isFading = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFading)
        {
            
            StartCoroutine(FadeOut());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {

        if (isFading)
        {
            isFading = false;
            yield return null;
        }

        isFading = true;
        currentFadeTime = 0.0f;

        while (isFading)
        {
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        if (isFading)
        {
            yield break; // Zaten fading iþlemi sürüyorsa çýk
        }

        isFading = true;
        currentFadeTime = 0.0f;

        Color endColor = new Color(0, 0, 0, 0);

        while (currentFadeTime <= fadeDuration)
        {
            currentFadeTime += Time.deltaTime;
            float t = Mathf.Clamp01(currentFadeTime / fadeDuration);

            // Karartma efektini kademeli olarak geri çevir
            blackMap.color = Color.Lerp(new Color(0, 0, 0, 1), endColor, t);

            yield return null;
        }
        blackMap.color = endColor;
        isFading = false;
    }

}
