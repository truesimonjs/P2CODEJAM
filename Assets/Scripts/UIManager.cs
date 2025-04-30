using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameObject uiPrefab; 
    public int maxPanels = 7;
    public Transform canvasTransform; 

    private List<GameObject> activePanels = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < maxPanels; i++)
        {
            SpawnPanel();
        }
    }

    public void SpawnPanel()
    {
        if (activePanels.Count >= maxPanels)
            return;

        Vector3 randomPos = new Vector3(
            Random.Range(-300f, 300f),
            Random.Range(-200f, 200f),
            0f
        );

        GameObject panel = Instantiate(uiPrefab, canvasTransform);
        panel.transform.localPosition = randomPos;

        // Setup CloseUI
        CloseUI closeScript = panel.GetComponent<CloseUI>();
        closeScript.manager = this;

        // Setup SwipeToClose
        SwipeToClose swipeScript = panel.GetComponent<SwipeToClose>();
        swipeScript.closeUI = closeScript;

        activePanels.Add(panel);
    }

    public void PanelClosed(GameObject panel)
    {
        activePanels.Remove(panel);
        SpawnPanel();
    }
}
