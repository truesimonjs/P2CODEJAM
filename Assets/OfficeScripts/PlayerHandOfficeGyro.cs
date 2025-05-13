using UnityEngine;

public class PlayerHandOfficeGyro : MonoBehaviour
{
    public float tiltSensitivity = 5f;
    public float maxOffset = 2f;
    public float moveSpeed = 5f;
    public float pushStrength = 10f;
    private Camera mainCam;
    private float fixedY = 0.6f; // Lock Y position
    private Vector3 handPos; // Hand Position
    private Vector3 initialAccel; 
    private Rigidbody rb;

    void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;

#if UNITY_ANDROID || UNITY_IOS
        // Sets initial position
        initialAccel = Input.acceleration;
#endif

        rb = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = false;
        rb.mass = 5f;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        // Lock rotation on rb
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Set initial rotation
        rb.rotation = Quaternion.Euler(0, 180, 0);

        Vector3 screenCenter = new Vector3(Screen.width / 2f, fixedY, Mathf.Abs(mainCam.transform.position.z));
        handPos = mainCam.ScreenToWorldPoint(screenCenter);
        handPos.y = fixedY;
    }

    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        // Uses accelerometer to set position of attatched object
        Vector3 accel = initialAccel - Input.acceleration; // Inverted movement
        Vector3 screenCenter = mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2f, fixedY, Mathf.Abs(mainCam.transform.position.z)));
        Vector3 offset = new Vector3(-accel.x, 0f, accel.z) * tiltSensitivity; // Z-axis movement inverted
        offset = Vector3.ClampMagnitude(offset, maxOffset);
        // Shifts object based on phone tilt
        Vector3 targetPos = screenCenter + offset;
        // Lerp smoothens transition between positions
        handPos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10f);
#endif

        handPos.y = fixedY;
        rb.linearVelocity = (handPos - rb.position) * moveSpeed;

        // Maintain flipped rotation
        rb.rotation = Quaternion.Euler(0, 180, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collided with: {collision.gameObject.name}");

        Rigidbody otherRb = collision.rigidbody;
        if (otherRb != null)
        {
            Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
            otherRb.AddForce(pushDirection * pushStrength, ForceMode.Impulse);
            Debug.Log($"Applied force to: {collision.gameObject.name}");
        }
    }

    public void RecenterTilt()
    {
#if UNITY_ANDROID || UNITY_IOS
        initialAccel = Input.acceleration;
#endif
    }
}