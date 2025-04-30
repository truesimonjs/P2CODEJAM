using UnityEngine;

public class OutfitEvaluator : MonoBehaviour
{
    public OutfitManager outfitManager;
    public int winThreshold = 10; // Example threshold

    public void EvaluateOutfit()
    {
        int totalScore = 0;
        foreach (var kvp in outfitManager.GetEquippedItems())
        {
            var item = kvp.Value.GetComponent<ClothingItem>();
            totalScore += item.stylePoints;
        }

        if (totalScore >= winThreshold)
        {
            Debug.Log("You win! Great outfit!");
        }
        else
        {
            Debug.Log("You lose! Try a better combination!");
        }
    }

}
