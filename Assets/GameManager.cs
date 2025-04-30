using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public string targetScene;

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
}
