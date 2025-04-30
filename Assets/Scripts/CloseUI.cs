using UnityEngine;
using UnityEngine.UI;

public class CloseUI : MonoBehaviour
{
    public Button closeButton; // Assign this in Inspector
    public UIManager manager;  // Manager reference

    void Start()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(ClosePanel);
    }

    public void ClosePanel()
    {
        if (manager != null)
            manager.PanelClosed(gameObject); // Notify manager

        Destroy(gameObject); // Remove the panel
    }
}
