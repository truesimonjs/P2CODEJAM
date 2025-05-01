using UnityEngine;

public class CloseUIButton : MonoBehaviour
{
    // Reference to the parent UI panel
    public GameObject uiPanelToClose;

    void Awake()
    {
        // If not assigned manually, default to the parent GameObject
        if (uiPanelToClose == null)
        {
            uiPanelToClose = transform.parent.gameObject;
        }
    }

    // This function will be called when the button is clicked
    public void CloseUI()
    {
        if (uiPanelToClose != null)
        {
            uiPanelToClose.SetActive(false);
        }
        else
        {
            Debug.LogWarning("UI Panel to close is not assigned.");
        }
    }
}
