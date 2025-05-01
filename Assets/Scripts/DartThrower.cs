using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DartThrow : MonoBehaviour
{
    public GameObject redDotPrefab;
    public Transform crosshair;
    public Dartboard dartboard;
    public TMP_Text scoreText;
    public float SceneChangeDelay = 1.5f;
    [SerializeField] private string targetScene;
    [SerializeField] private string targetLoseScene = "MainMenu";

    private int totalScore = 0;
    private int throwsMade = 0;
    private int maxThrows = 3;

    [System.Obsolete]
    public void ThrowDart()
    {
        if (throwsMade >= maxThrows)
            return;

        Vector3 hitPosition = crosshair.position;

        Instantiate(redDotPrefab, hitPosition, Quaternion.identity);

        if (dartboard != null)
        {
            int score = dartboard.CalculateScore(hitPosition);
            totalScore += score;
            throwsMade++;
            UpdateScoreUI();

            if (throwsMade == maxThrows)
            {
                CheckWinLose();
            }
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + totalScore;
    }

    [System.Obsolete]
    void CheckWinLose()
    {

        if (totalScore >= 50)
        {
            Win();
            Debug.Log("You Win");
        }
        else
        {
            Lose();
            Debug.Log("You Lose");
        }
    }

    [System.Obsolete]
    public void Win()
    {
        StartCoroutine(WinSequence());
    }
    [System.Obsolete]
    private IEnumerator WinSequence()
    {
        yield return new WaitForSeconds(SceneChangeDelay);
        SceneTransitionManager.Instance.TransitionToScene(targetScene);
        // TODO: Trigger win condition logic
    }

    [System.Obsolete]
    public void Lose()
    {
        StartCoroutine(LoseSequence());
    }

    [System.Obsolete]
    private IEnumerator LoseSequence()
    {
        yield return new WaitForSeconds(SceneChangeDelay);
        SceneTransitionManager.Instance.TransitionToScene(targetLoseScene);
        // TODO: Trigger win condition logic
    }

}
