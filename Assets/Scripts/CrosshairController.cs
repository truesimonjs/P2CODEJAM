using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public float noiseAmount = 0.1f;
    private Camera mainCam;
    private float zPlane = -1;

    void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get mouse position in screen space
        Vector3 mouseScreenPos = Input.mousePosition;

        // Ensure correct Z for screen-to-world conversion
        mouseScreenPos.z = Mathf.Abs(mainCam.transform.position.z - zPlane);

        // Convert to world space
        Vector3 worldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);

        // Add noise
        worldPos.x += Random.Range(-noiseAmount, noiseAmount);
        worldPos.y += Random.Range(-noiseAmount, noiseAmount);

        // Clamp to camera world bounds
        Vector3 clampedPos = ClampToCameraBounds(worldPos);

        // Set final position
        clampedPos.z = zPlane;
        transform.position = clampedPos;
    }

    Vector3 ClampToCameraBounds(Vector3 position)
    {
        Vector3 min = mainCam.ViewportToWorldPoint(new Vector3(0, 0, Mathf.Abs(mainCam.transform.position.z - zPlane)));
        Vector3 max = mainCam.ViewportToWorldPoint(new Vector3(1, 1, Mathf.Abs(mainCam.transform.position.z - zPlane)));

        position.x = Mathf.Clamp(position.x, min.x, max.x);
        position.y = Mathf.Clamp(position.y, min.y, max.y);

        return position;
    }
}
