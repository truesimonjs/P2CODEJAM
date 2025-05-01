using UnityEngine;

public class LandGenerator : MonoBehaviour
{
    public GameObject landSegmentPrefab;
    public int segmentCount = 10;

    private Transform lastEndPoint;

    void Start()
    {
        GenerateLand();
    }

    void GenerateLand()
    {
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject segment = Instantiate(landSegmentPrefab);

            if (lastEndPoint == null)
            {
                // First segment at origin
                segment.transform.position = transform.position;
            }
            else
            {
                // Place the new segment at the previous one's EndPoint
                segment.transform.position = lastEndPoint.position;
            }

            // Find the EndPoint child in the new segment
            lastEndPoint = segment.transform.Find("EndPoint");
            if (lastEndPoint == null)
            {
                Debug.LogError("Prefab must have a child named 'EndPoint'");
                break;
            }
        }
    }
}
