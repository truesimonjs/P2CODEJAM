using UnityEngine;

public class SwipeToClose : MonoBehaviour
{
    public CloseUI closeUI; // Reference to CloseUI
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;

    private float swipeThreshold = 0.2f; // 20% screen width

    void Update()
    {
        // Touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isSwiping = true;
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    if (isSwiping)
                    {
                        endTouchPosition = touch.position;
                        DetectSwipe();
                        isSwiping = false;
                    }
                    break;
            }
        }

        // Mouse (for testing)
        if (Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
            startTouchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            endTouchPosition = Input.mousePosition;
            DetectSwipe();
            isSwiping = false;
        }
    }

    void DetectSwipe()
    {
        Vector2 delta = endTouchPosition - startTouchPosition;
        float distance = delta.magnitude;

        if (distance > Screen.width * swipeThreshold)
        {
            Debug.Log("Swipe detected! Closing panel.");
            if (closeUI != null)
                closeUI.ClosePanel();
        }
    }
}
