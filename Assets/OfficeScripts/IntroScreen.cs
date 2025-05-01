using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroScreen : MonoBehaviour
{
    public Image panel;   // Background panel
    public Image icon1;   // First icon
    public Image icon2;   // Second icon
    public float popDuration = 0.2f;
    public float fadeDuration = 2f;
    public float popSizeMultiplier = 1.2f; // How much larger the images should pop in

    void Start()
    {
        StartCoroutine(PopInAndFadeOut());
    }

    IEnumerator PopInAndFadeOut()
    {
        // Step 1: Pop-in effect for panel and icons using RectTransform
        float elapsedTime = 0f;
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        RectTransform icon1Rect = icon1.GetComponent<RectTransform>();
        RectTransform icon2Rect = icon2.GetComponent<RectTransform>();

        Vector2 originalSizePanel = panelRect.sizeDelta;
        Vector2 originalSizeIcon1 = icon1Rect.sizeDelta;
        Vector2 originalSizeIcon2 = icon2Rect.sizeDelta;

        Vector2 targetSizePanel = originalSizePanel * popSizeMultiplier;
        Vector2 targetSizeIcon1 = originalSizeIcon1 * popSizeMultiplier;
        Vector2 targetSizeIcon2 = originalSizeIcon2 * popSizeMultiplier;

        while (elapsedTime < popDuration)
        {
            elapsedTime += Time.deltaTime;
            panelRect.sizeDelta = Vector2.Lerp(originalSizePanel, targetSizePanel, elapsedTime / popDuration);
            icon1Rect.sizeDelta = Vector2.Lerp(originalSizeIcon1, targetSizeIcon1, elapsedTime / popDuration);
            icon2Rect.sizeDelta = Vector2.Lerp(originalSizeIcon2, targetSizeIcon2, elapsedTime / popDuration);
            yield return null;
        }

        // Step 2: Fade-out effect for panel and icons
        elapsedTime = 0f;
        Color panelColor = panel.color;
        Color icon1Color = icon1.color;
        Color icon2Color = icon2.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            panelColor.a = alpha;
            icon1Color.a = alpha;
            icon2Color.a = alpha;

            panel.color = panelColor;
            icon1.color = icon1Color;
            icon2.color = icon2Color;
            yield return null;
        }
    }
}