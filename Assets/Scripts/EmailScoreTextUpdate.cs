using UnityEngine;
using TMPro;

public class EmailScoreTextUpdate : MonoBehaviour
{
    [Header("Assign the TMP text component in Inspector")]
    public TMP_Text scoreText;

    [Header("Reference to GameManagerOffice")]
    public GameManagerOffice gameManagerOffice;

    void Start()
    {
        // Ensure GameManagerOffice is assigned
        if (gameManagerOffice == null)
        {
            gameManagerOffice = FindObjectOfType<GameManagerOffice>();

            if (gameManagerOffice == null)
            {
                Debug.LogWarning("GameManagerOffice reference not found in the scene.");
            }
        }

        // Initial update
        UpdateScoreDisplay();
    }

    void Update()
    {
        // Update the score display every frame (consider optimizing for performance)
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null && gameManagerOffice != null)
        {
            scoreText.text = "" + gameManagerOffice.OfficeScore;
        }
    }
}