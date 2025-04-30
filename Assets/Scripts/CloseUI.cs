using UnityEngine;

public class CloseUI : MonoBehaviour
{
    public GameObject uiElement; // Drag your panel or UI object here

    public void ClosePanel()
    {
        uiElement.SetActive(false); // Hides the UI
    }
}
