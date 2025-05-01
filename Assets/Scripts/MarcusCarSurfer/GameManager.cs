using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject winUI;
    public string targetScene;
    public float SceneChangeDelay = 2f;
    [SerializeField] private string targetWinScene;
    [SerializeField] private string targetLoseScene;


    public void WinGame()
    {
        winUI.SetActive(true);
        Time.timeScale = 0f; // Pause game
        Win();
    }

    public void EndGame()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pauses the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((targetScene));
    }
    
    [System.Obsolete]
    public void Win()
    {
        StartCoroutine(WinSequence());
    }

    [System.Obsolete]
    private IEnumerator WinSequence()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(SceneChangeDelay);
        SceneTransitionManager.Instance.TransitionToScene(targetWinScene);
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
        Time.timeScale = 1f;
    }
}
