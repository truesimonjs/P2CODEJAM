using UnityEngine;

public class BalanceGame : MonoBehaviour
{
    [Header("Scoop Settings")]
    public GameObject[] iceCreamPrefabs;
    public int maxScoops = 5;
    public float scoopInterval = 5f;

    [Header("Balance Settings")]
    public float torqueStrength = 5f;
    public float maxTiltAngle = 30f;
    public float angularDamping = 1.5f;

    [Header("Game Timer")]
    public float gameDuration = 30f;

    private int currentScoops = 0;
    private float scoopTimer = 0f;
    private float gameTimer = 0f;
    private bool gameOver = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 20f;
    }

    void FixedUpdate()
    {
        if (gameOver) return;

        float input = GetInput();
        float difficultyMultiplier = 1 + currentScoops * 0.1f;

        rb.AddTorque(Vector3.back * input * torqueStrength * difficultyMultiplier, ForceMode.Force);
        rb.angularVelocity *= (1 - Time.fixedDeltaTime * angularDamping);

        Vector3 rot = transform.rotation.eulerAngles;
        if (rot.z > 180f) rot.z -= 360f;
        rot.z = Mathf.Clamp(rot.z, -maxTiltAngle, maxTiltAngle);
        transform.rotation = Quaternion.Euler(0f, 0f, rot.z);
    }

    void Update()
    {
        if (gameOver) return;

        // Add scoop over time
        if (currentScoops < maxScoops)
        {
            scoopTimer += Time.deltaTime;
            if (scoopTimer >= scoopInterval)
            {
                AddIceCream();
                scoopTimer = 0f;
            }
        }

        // Update game timer
        gameTimer += Time.deltaTime;
        if (gameTimer >= gameDuration)
        {
            EndGame();
            gameOver = true;
        }
    }

    float GetInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetAxis("Horizontal");
#elif UNITY_ANDROID || UNITY_IOS
        return Input.acceleration.x * 2f;
#else
        return 0f;
#endif
    }

    void AddIceCream()
    {
        if (iceCreamPrefabs.Length == 0) return;

        Vector3 spawnPos = transform.position + Vector3.up * (1.2f + currentScoops * 0.6f);
        spawnPos.x += Random.Range(-0.2f, 0.2f);

        GameObject chosenPrefab = iceCreamPrefabs[Random.Range(0, iceCreamPrefabs.Length)];
        GameObject scoop = Instantiate(chosenPrefab, spawnPos, Quaternion.identity);
        scoop.transform.localScale = Vector3.one * 0.5f;

        Rigidbody scoopRb = scoop.GetComponent<Rigidbody>();
        if (scoopRb != null)
        {
            scoopRb.mass = 0.15f;
        }

        // Important: Tag your prefab as "Scoop"
        scoop.tag = "BalanceObj";

        currentScoops++;
    }

    void EndGame()
    {
        int stillOnBoard = 0;
        GameObject[] scoops = GameObject.FindGameObjectsWithTag("BalanceObj");

        foreach (GameObject scoop in scoops)
        {
            float verticalDist = scoop.transform.position.y - transform.position.y;
            float horizontalDist = Mathf.Abs(scoop.transform.position.x - transform.position.x);

            if (verticalDist > 0 && verticalDist < 4f && horizontalDist < 1.5f)
            {
                stillOnBoard++;
            }
        }

        if (stillOnBoard >= 5)
        {
            Debug.Log("You Win! Scoops Balanced: " + stillOnBoard);
        }
        else
        {
            Debug.Log("You Lose! Scoops Balanced: " + stillOnBoard);
        }
    }
}
