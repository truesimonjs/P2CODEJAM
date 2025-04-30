using UnityEngine;

public class LaneMover : MonoBehaviour
{
    public float laneOffset = 3f; // Distance between lanes on the X axis
    public float moveSpeed = 10f;

    private int currentLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        // Input: Left arrow or A
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane > 0)
                currentLane--;
        }

        // Input: Right arrow or D
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane < 2)
                currentLane++;
        }

        // Set target X position based on currentLane
        float targetX = (currentLane - 1) * laneOffset;
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        // Smooth move to target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
}
