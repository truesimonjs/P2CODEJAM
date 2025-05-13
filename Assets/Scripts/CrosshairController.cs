/*using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public float tiltSensitivity = 5f;     // Controls responsiveness to tilt
    public float noiseAmount = 0.1f;       // Adds slight shake
    public float maxOffset = 2f;           // Max distance from screen center in world units
    private Camera mainCam;
    private float zPlane = -1f;
    private Vector3 crosshairPos;
    private Vector3 initialAccel;

    void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;

#if UNITY_ANDROID || UNITY_IOS
        initialAccel = Input.acceleration;
#endif

        // Set initial crosshair position at screen center
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, Mathf.Abs(mainCam.transform.position.z - zPlane));
        crosshairPos = mainCam.ScreenToWorldPoint(screenCenter);
        crosshairPos.z = zPlane;
    }

    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        // Directly map tilt to offset from center
        Vector3 accel = Input.acceleration - initialAccel;

        Vector3 screenCenter = mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Mathf.Abs(mainCam.transform.position.z - zPlane)));
        Vector3 offset = new Vector3(accel.x, accel.y, 0f) * tiltSensitivity;
        offset = Vector3.ClampMagnitude(offset, maxOffset);

        Vector3 targetPos = screenCenter + offset;
        crosshairPos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10f); // smooth movement

#else
        // Mouse-based input
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(mainCam.transform.position.z - zPlane);
        crosshairPos = mainCam.ScreenToWorldPoint(mouseScreenPos);
#endif

        // Add noise
        crosshairPos.x += Random.Range(-noiseAmount, noiseAmount);
        crosshairPos.y += Random.Range(-noiseAmount, noiseAmount);

        // Clamp to camera view
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

    // Optional: Call this from a UI button to recalibrate
    public void RecenterTilt()
    {
#if UNITY_ANDROID || UNITY_IOS
        initialAccel = Input.acceleration;
#endif
    }
}*/