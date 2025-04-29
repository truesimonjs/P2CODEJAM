using UnityEngine;
using TMPro;

public class DocumentBehaviour : MonoBehaviour
{
    public Transform desk;
    public GameManagerOffice gameManagerOffice;
    public TMP_Text scoreText;
    public DocumentPool documentPool;

    [SerializeField] private float DistanceFromDesk = 25f;

    void Update()
    {
        // Ensure we're comparing positions in world space
        Vector3 deskWorldPos = desk.position;

        // If this object is parented, use the parent's world position
        Vector3 objectWorldPos = transform.position;

        if (Vector3.Distance(objectWorldPos, deskWorldPos) > DistanceFromDesk)
        {
            gameManagerOffice.OfficeScore++;
            scoreText.text = gameManagerOffice.OfficeScore.ToString();
            Debug.Log(gameManagerOffice.OfficeScore);
            
            documentPool.AddToPool(gameObject);
        }
    }
}