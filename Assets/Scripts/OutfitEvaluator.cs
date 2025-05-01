using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OutfitEvaluator : MonoBehaviour
{
    public OutfitManager outfitManager;
    public int winThreshold = 10; // Example threshold
    public TextMeshProUGUI resultText;

    // Customize your scene names here
    [SerializeField] private string nextSceneName = "NextLevelScene";
    [SerializeField] private string menuSceneName = "MainMenu";

    public void EvaluateOutfit()
    {
        int totalScore = CalculateTotalScore();

        Debug.Log($"Total Outfit Score: {totalScore}");

        if (totalScore >= winThreshold)
        {
            HandleWin();
        }
        else
        {
            HandleLose();
        }
    }

    private int CalculateTotalScore()
    {
        int score = 0;
        foreach (var kvp in outfitManager.GetEquippedItems())
        {
            var item = kvp.Value.GetComponent<ClothingItem>();
            if (item != null)
            {
                score += item.stylePoints;
            }
        }
        return score;
    }

    private void HandleWin()
    {
        Debug.Log("You win! Great outfit!");
        ShowMessageAndLoadScene("You Win!", nextSceneName);
        // TODO: Add win UI, sound, animation, etc.
        // win = you win message and move to next scene
    }

    private void HandleLose()
    {
        Debug.Log("You lose! Try a better combination!");
        ShowMessageAndLoadScene("You Lose!", menuSceneName);
        // TODO: Add lose UI, sound, hint, etc.
        // lose = lose message and move back to menu
    }

    private void ShowMessageAndLoadScene(string message, string sceneName)
    {
        if (resultText != null)
        {
            resultText.text = message;
            resultText.gameObject.SetActive(true);
        }

        StartCoroutine(LoadSceneAfterDelay(sceneName, 2f));

        // OPTIONAL: You can delay scene load to show message with Invoke
        //Invoke(nameof(() => SceneManager.LoadScene(sceneName)), 2f); // Delay 2 seconds
    }

    private System.Collections.IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
