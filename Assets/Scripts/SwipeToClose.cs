using UnityEngine;

public class SwipeToClose : MonoBehaviour
{
    public CloseUI closeUI; // Reference to your CloseUI script
    public GameObject uiElement; // The UI panel you want to move
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;

    private Vector3 originalPosition; // To store the original position of the UI element

    private float swipeThreshold = 0.2f; // Sensitivity for when to close the UI
    private float swipeSpeed = 0.5f; // Speed of the swipe (adjust for fluidity)

    void Start()
    {
        originalPosition = uiElement.transform.position; // Store the initial position
    }

    void Update()
    {
        // Touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isSwiping = true;
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        endTouchPosition = touch.position;
                        MoveUI();
                    }
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

        // Mouse input (for testing in the editor)
        if (Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
            startTouchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && isSwiping)
        {
            endTouchPosition = Input.mousePosition;
            MoveUI();
        }
        if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            DetectSwipe();
            isSwiping = false;
        }
    }

    // Move the UI element based on the swipe
    void MoveUI()
    {
        // Calculate how far the user has moved
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;
        // Apply the swipe speed to make it feel more fluid
        Vector3 newPos = originalPosition + new Vector3(swipeDelta.x * swipeSpeed, swipeDelta.y * swipeSpeed, 0f);

        // Move the UI element smoothly
        uiElement.transform.position = newPos;
    }

    // Detect if the swipe is enough to trigger the close
    // Detect swipe completion
void DetectSwipe()
{
    // Calculate the distance of the swipe
    Vector2 delta = endTouchPosition - startTouchPosition;
    float distance = delta.magnitude;

    // Check if the swipe is long enough
    if (distance > Screen.width * swipeThreshold)
    {
        float angle = Vector2.Angle(Vector2.right, delta);
        
        // Example: trigger close if swiped left (mostly horizontal)
        if (angle > 135f || angle < 45f) // horizontal swipe
        {
            closeUI.ClosePanel(); // Close the UI
        }
    }
    else
    {
        // If swipe is too small, reset to the original position
        uiElement.transform.position = originalPosition;
    }
}

}
