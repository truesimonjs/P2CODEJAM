using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayTracker : MonoBehaviour
{
    public static DayTracker Instance;

    private int currentDay = 0;
    private string targetScene = "MainMenu";

    [System.Obsolete]
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Obsolete]
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetScene)
        {
            currentDay++;
            StartCoroutine(UpdateDayTextReliable());
        }
    }

    [System.Obsolete]
    private System.Collections.IEnumerator UpdateDayTextReliable()
    {
        yield return null; // wait one frame for scene objects to load

        TextMeshProUGUI[] texts = GameObject.FindObjectsOfType<TextMeshProUGUI>(true);
        foreach (var tmp in texts)
        {
            if (tmp.name == "DayTracker")
            {
                tmp.text = "Day " + currentDay;
                Debug.Log("Updated DayTracker text to: Day " + currentDay);
                yield break;
            }
        }

        Debug.LogWarning("Could not find a TextMeshProUGUI named 'DayTracker'.");
    }

    [System.Obsolete]
    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
