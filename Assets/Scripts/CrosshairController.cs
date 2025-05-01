using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public float noiseAmount = 0.1f;
    public float tiltSensitivity = 5f; // Adjust for accelerometer movement
    private Camera mainCam;
    private float zPlane = -1f;
    private Vector3 crosshairPos;
    private Vector3 initialAccel;

    void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;

        // Capture the initial accelerometer position
#if UNITY_ANDROID || UNITY_IOS
        initialAccel = Input.acceleration;
#endif

        // Start at screen center
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, Mathf.Abs(mainCam.transform.position.z - zPlane));
        crosshairPos = mainCam.ScreenToWorldPoint(screenCenter);
        crosshairPos.z = zPlane;
    }

    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        // Use accelerometer tilt input relative to initial position
        Vector3 accel = Input.acceleration - initialAccel;
        crosshairPos.x += accel.x * tiltSensitivity * Time.deltaTime;
        crosshairPos.y += accel.y * tiltSensitivity * Time.deltaTime;
#else
        // Use mouse position on PC
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(mainCam.transform.position.z - zPlane);
        crosshairPos = mainCam.ScreenToWorldPoint(mouseScreenPos);
#endif

        // Add screen shake noise
        crosshairPos.x += Random.Range(-noiseAmount, noiseAmount);
        crosshairPos.y += Random.Range(-noiseAmount, noiseAmount);

        // Clamp position to camera bounds
        Vector3 clamped = ClampToCameraBounds(crosshairPos);
        clamped.z = zPlane;
        transform.position = clamped;
    }

    Vector3 ClampToCameraBounds(Vector3 pos)
    {
        float camZ = Mathf.Abs(mainCam.transform.position.z - zPlane);
        Vector3 min = mainCam.ViewportToWorldPoint(new Vector3(0, 0, camZ));
        Vector3 max = mainCam.ViewportToWorldPoint(new Vector3(1, 1, camZ));

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        return pos;
    }
}