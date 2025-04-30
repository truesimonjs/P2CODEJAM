using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class QTEManager : MonoBehaviour
{
    public TMP_Text qteText;
    public float timeLimit = 5f;
    public int sequenceLength = 3;
    public List<string> possibleButtons = new List<string> { "A", "S", "D", "W" };

    public Action OnQTESuccess;
    public Action OnQTEFailure;

    private List<string> qteSequence = new List<string>();
    private int currentIndex = 0;
    private float timer;
    private bool qteActive = false;

    public void StartQTE()
    {
        GenerateSequence();
        currentIndex = 0;
        qteActive = true;
        timer = timeLimit;
        UpdateDisplay();
    }

    void GenerateSequence()
    {
        qteSequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            qteSequence.Add(possibleButtons[UnityEngine.Random.Range(0, possibleButtons.Count)]);
        }
    }

    void UpdateDisplay()
    {
        qteText.text = "<color=yellow>Press:</color> ";
        for (int i = 0; i < qteSequence.Count; i++)
        {
            if (i < currentIndex)
                qteText.text += $"<color=green>{qteSequence[i]}</color> ";
            else
                qteText.text += qteSequence[i] + " ";
        }
    }

    void Update()
    {
        if (!qteActive) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            qteActive = false;
            qteText.text = "<color=red>Too slow!</color>";
            OnQTEFailure?.Invoke();
        }
    }

    public void PressButton(string button)
    {
        if (!qteActive) return;

        if (button == qteSequence[currentIndex])
        {
            currentIndex++;
            UpdateDisplay();

            if (currentIndex >= qteSequence.Count)
            {
                qteActive = false;
                qteText.text = "<color=green>Success!</color>";
                OnQTESuccess?.Invoke();
            }
        }
        else
        {
            qteActive = false;
            qteText.text = "<color=red>Wrong button!</color>";
            OnQTEFailure?.Invoke();
        }
    }

    public void ShowQTE()
    {
        qteText.gameObject.SetActive(true);
    }

    public void HideQTE()
    {
        qteText.gameObject.SetActive(false);
    }
}
