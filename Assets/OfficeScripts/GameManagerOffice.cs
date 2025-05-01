using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerOffice : MonoBehaviour
{
    public static GameManagerOffice instance;
    public int OfficeScore;
    public int WinScore = 200;

    public GameObject Hand;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public float LoseTime = 60f; // Countdown timer
    
    private bool hasWon = false;
    private bool hasLost = false;

    private float currentLoseTime;
    
    public float SceneChangeDelay = 2f;
    [SerializeField] private string targetWinScene;
    [SerializeField] private string targetLoseScene;

    // UI Elements for timer
    public Image LoseTimeFill; // UI Image for circular timer
    public TMP_Text LoseTimeText; // TextMeshPro for timer countdown
    
    void Start()
    {
        currentLoseTime = LoseTime;
        LoseTimeFill.fillAmount = currentLoseTime / LoseTime;
        hasWon = false;
        hasLost = false;
    }

    void Update()
    {
        // Clock rundown timer animation thingy
        if (currentLoseTime > 0 && hasWon == false)
        {
            currentLoseTime -= Time.deltaTime;

            // Update UI elements
            LoseTimeFill.fillAmount = currentLoseTime / LoseTime; // Adjust circular UI
            LoseTimeText.text = Mathf.Ceil(currentLoseTime).ToString(); // Display countdown

        }
        // Lose when time runs out
        else if (currentLoseTime <= 0 && hasWon == false)
        {
            LoseScreen.SetActive(true);
            Hand.SetActive(false);
            hasLost = true;
        }
        // Win if score quota is met
        
        if (OfficeScore >= WinScore && hasLost == false)
        {
            WinScreen.SetActive(true);
            Hand.SetActive(false);
            hasWon = true;
            Win();
        }
    }
    
    public void Win()
    {
        StartCoroutine(WinSequence());
    }

    [System.Obsolete]
    private IEnumerator WinSequence()
    {
        Time.timeScale = 1f;
        hasWon = false;
        hasLost = false;
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
    
    // Reload scene if u lose
    public void RestartOfficeScene()
    {
        currentLoseTime = LoseTime; // Reset countdown when restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}