using System.Collections.Generic;
using UnityEngine;

public class UISpawner : MonoBehaviour
{
    [Header("Assign your UI prefab here in Inspector")]
    public GameObject uiPrefab;

    [Header("Assign the parent (like a Canvas or Panel)")]
    public Transform parentTransform;

    [Header("Spawning Settings")]
    public float spawnInterval = 2f; // Time in seconds between spawns
    public float padding = 50f; // Adjustable padding in pixels

    [Header("Limit Settings")]
    public int maxSpawnCount = 7; // Max number of UI objects allowed on screen

    private float timer;

    // Track spawned objects
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        // Only spawn if we have fewer than the max allowed
        if (timer >= spawnInterval)
        {
            // If we have less than max UI objects, spawn a new one
            if (spawnedObjects.Count < maxSpawnCount)
            {
                SpawnUI();
            }
            else
            {
                // Optionally, remove objects that are no longer active
                CleanUpInactiveObjects();
            }

            timer = 0f; // Reset the timer
        }
    }

    void SpawnUI()
    {
        if (uiPrefab != null && parentTransform != null)
        {
            GameObject spawnedUI = Instantiate(uiPrefab, parentTransform);

            // Add to tracking list
            spawnedObjects.Add(spawnedUI);

            RectTransform rect = spawnedUI.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.localScale = Vector3.one;
                rect.localRotation = Quaternion.identity;

                // Get parent RectTransform (Canvas or Panel)
                RectTransform parentRect = parentTransform.GetComponent<RectTransform>();
                if (parentRect != null)
                {
                    // Get the safe area from Screen.safeArea (in pixels)
                    Rect safeArea = Screen.safeArea;

                    // Convert the safe area to Canvas units
                    Vector2 anchorMin = safeArea.position;
                    Vector2 anchorMax = safeArea.position + safeArea.size;

                    anchorMin.x /= Screen.width;
                    anchorMin.y /= Screen.height;
                    anchorMax.x /= Screen.width;
                    anchorMax.y /= Screen.height;

                    // Calculate the safe area size in Canvas space
                    float safeWidth = parentRect.rect.width * (anchorMax.x - anchorMin.x);
                    float safeHeight = parentRect.rect.height * (anchorMax.y - anchorMin.y);

                    float safeX = parentRect.rect.width * anchorMin.x - parentRect.rect.width / 2f;
                    float safeY = parentRect.rect.height * anchorMin.y - parentRect.rect.height / 2f;

                    // Add padding to avoid edges
                    safeWidth -= padding * 2f;
                    safeHeight -= padding * 2f;
                    safeX += padding;
                    safeY += padding;

                    // Random position inside safe area with padding, ensuring it doesn't go off-screen
                    Vector2 randomPos = new Vector2(
                        Mathf.Clamp(Random.Range(safeX, safeX + safeWidth), safeX, safeX + safeWidth),
                        Mathf.Clamp(Random.Range(safeY, safeY + safeHeight), safeY, safeY + safeHeight)
                    );

                    rect.anchoredPosition = randomPos;
                }
            }
        }
        else
        {
            Debug.LogWarning("Prefab or Parent Transform is not assigned.");
        }
    }

    // Clean up inactive objects that are not being used anymore
    void CleanUpInactiveObjects()
    {
        spawnedObjects.RemoveAll(obj => obj == null || !obj.activeInHierarchy);
    }
}
