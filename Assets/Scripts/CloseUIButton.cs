using UnityEngine;

public class CloseUIButton : MonoBehaviour
{
    [Header("Assign the parent UI panel (optional)")]
    public GameObject uiPanelToClose;

    private GameManagerOffice gameManagerOffice;

    void Awake()
    {
        // Default to parent GameObject if not manually assigned
        if (uiPanelToClose == null)
        {
            uiPanelToClose = transform.parent.gameObject;
        }

        // Automatically find GameManagerOffice in the scene
        gameManagerOffice = FindObjectOfType<GameManagerOffice>();

        if (gameManagerOffice == null)
        {
            Debug.LogWarning("GameManagerOffice reference not found in the scene.");
        }
    }

    public void CloseUI()
    {
        if (!uiPanelToClose)
        {
            Debug.LogWarning("UI panel reference is missing.");
            return;
        }

        uiPanelToClose.SetActive(false);

        if (gameManagerOffice)
        {
            gameManagerOffice.OfficeScore++;
            Debug.Log(gameManagerOffice.OfficeScore);
        }
        else
        {
            Debug.LogWarning("Cannot increase OfficeScore—GameManagerOffice reference is missing.");
        }
    }
}