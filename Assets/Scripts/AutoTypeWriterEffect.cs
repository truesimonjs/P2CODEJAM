using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class AutoTypewriterEffect : MonoBehaviour
{
    public float typingSpeed = 0.04f;

    private TMP_Text textComponent;
    private string lastText = "";
    private Coroutine typingCoroutine;

    void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    void OnEnable()
    {
        lastText = textComponent.text;
        StartTyping(lastText);
    }

    void Update()
    {
        if (textComponent.text != lastText)
        {
            lastText = textComponent.text;
            StartTyping(lastText);
        }
    }

    void StartTyping(string fullText)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(fullText));
    }

    IEnumerator TypeText(string text)
    {
        textComponent.maxVisibleCharacters = 0;

        for (int i = 0; i <= text.Length; i++)
        {
            textComponent.maxVisibleCharacters = i;

            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
