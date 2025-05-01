using UnityEngine;

public class Dartboard : MonoBehaviour
{
    public int maxScore = 50;
    public float radius = 2f; // Match half of your dartboard scale

    public int CalculateScore(Vector3 hitPoint)
    {
        float dist = Vector3.Distance(hitPoint, transform.position);
        float t = Mathf.Clamp01(dist / radius);
        return Mathf.RoundToInt(Mathf.Lerp(maxScore, 0, t));
    }
}
