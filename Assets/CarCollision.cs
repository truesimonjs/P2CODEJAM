using UnityEngine;

public class CarCollision : MonoBehaviour
{
    public GameManager gameManager;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) // Tag other cars as "Obstacle"
        {
            gameManager.EndGame();
        }
    }
}
