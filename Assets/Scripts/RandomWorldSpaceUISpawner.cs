using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RandomWorldSpaceUISpawner : MonoBehaviour
{
    public RectTransform uiPrefab;  // Your UI prefab to spawn
    public Camera mainCamera;
    public float spawnInterval = 2f;
    public float uiPadding = 100f;
    public float spawnDistance = 5f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnNewUI();
            timer = 0f;
        }
    }

    void SpawnNewUI()
    {
        // Random screen position
        float minX = uiPadding;
        float maxX = Screen.width - uiPadding;
        float minY = uiPadding;
        float maxY = Screen.height - uiPadding;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector3 screenPos = new Vector3(randomX, randomY, spawnDistance);
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);

        // ✅ Instantiate new UI clone
        RectTransform newUI = Instantiate(uiPrefab, worldPos, Quaternion.identity);

        // Ensure the new UI is part of the canvas in world space
        newUI.SetParent(uiPrefab.parent);
        newUI.localScale = Vector3.one;

        // Add Button Close functionality
        Button closeButton = newUI.GetComponentInChildren<Button>();
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => CloseUI(newUI.gameObject));
        }

        // Add Swipe functionality (using EventTrigger)
        AddSwipeToClose(newUI.gameObject);
    }

    void CloseUI(GameObject uiToClose)
    {
        uiToClose.SetActive(false); // Hides the UI
    }

    void AddSwipeToClose(GameObject uiObject)
    {
        // Add EventTrigger for swipe functionality
        EventTrigger eventTrigger = uiObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = uiObject.AddComponent<EventTrigger>();
        }

        // Detect swipe (basic example: swipe left to close)
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((eventData) => OnSwipe(eventData, uiObject));
        eventTrigger.triggers.Add(entry);
    }

    void OnSwipe(BaseEventData eventData, GameObject uiObject)
    {
        PointerEventData pointerData = (PointerEventData)eventData;
        
        // If swipe moves left, close the UI (you can adjust threshold for swipe sensitivity)
        if (pointerData.delta.x < -50)  // You can tweak the threshold here
        {
            CloseUI(uiObject);
        }
    }
}
