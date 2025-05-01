using UnityEngine;

public class LaneMover : MonoBehaviour
{
    public float laneOffset = 3f; // Distance between lanes on the X axis
    public float moveSpeed = 10f;

    private int currentLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private Vector3 targetPosition;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f; // Minimum distance to count as a swipe

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        // Keyboard controls (for testing on PC)
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane > 0)
                currentLane--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane < 2)
                currentLane++;
        }

        // Touch controls (mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                Vector2 swipe = endTouchPosition - startTouchPosition;

                if (Mathf.Abs(swipe.x) > swipeThreshold && Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                {
                    if (swipe.x > 0)
                    {
                        // Swipe right
                        if (currentLane < 2)
                            currentLane++;
                    }
                    else
                    {
                        // Swipe left
                        if (currentLane > 0)
                            currentLane--;
                    }
                }
            }
        }

        // Set and move toward target position
        float targetX = (currentLane - 1) * laneOffset;
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
}
