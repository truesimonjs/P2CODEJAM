using UnityEngine;

public class DocumentSpawner : MonoBehaviour
{
    public DocumentPool documentPool;  // Reference to the object pool
    public Vector3 spawnAreaMin;   // Minimum bounds for random spawn
    public Vector3 spawnAreaMax;   // Maximum bounds for random spawn
    public float spawnInterval = 3f;  // Time interval between spawns

    private void Start()
    {
        InvokeRepeating(nameof(SpawnDocument), spawnInterval, spawnInterval);
    }

    private void SpawnDocument()
    {
        GameObject document = documentPool.GetFromPool();
        if (document != null)
        {
            // Generate a random position within the defined range
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomZ = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
            Vector3 randomPosition = new Vector3(randomX, transform.position.y, randomZ);

            document.transform.position = randomPosition;  // Set position
            document.SetActive(true);  // Reactivate document
        }
    }
}