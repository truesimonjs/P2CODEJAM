using UnityEngine;
using TMPro;
using UnityEngine.UI;
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

    private float currentLoseTime;

    // UI Elements
    public Image LoseTimeFill; // UI Image for circular timer
    public TMP_Text LoseTimeText; // TextMeshPro for timer countdown
    
    void Start()
    {
        currentLoseTime = LoseTime;
        LoseTimeFill.fillAmount = currentLoseTime / LoseTime;
    }

    void Update()
    {
        if (currentLoseTime > 0 && hasWon == false)
        {
            currentLoseTime -= Time.deltaTime;

            // Update UI elements
            LoseTimeFill.fillAmount = currentLoseTime / LoseTime; // Adjust circular UI
            LoseTimeText.text = Mathf.Ceil(currentLoseTime).ToString(); // Display countdown

        }
        else if (currentLoseTime <= 0 && hasWon == false)
        {
            LoseScreen.SetActive(true);
            Hand.SetActive(false);
        }

        if (OfficeScore > WinScore && hasWon == false)
        {
            WinScreen.SetActive(true);
            Hand.SetActive(false);
            hasWon = true;
        }
    }
    
    public void RestartOfficeScene()
    {
        currentLoseTime = LoseTime; // Reset countdown when restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}