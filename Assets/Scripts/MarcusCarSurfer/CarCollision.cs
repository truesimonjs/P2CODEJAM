using UnityEngine;

public class CarCollision : MonoBehaviour
{
    public GameManager gameManager;

    void OnCollisionEnter(Collision collision)
    {
        // Tag other cars as "Obstacle"
        if (collision.gameObject.CompareTag("Obstacle")) 
        {
            gameManager.EndGame();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinCarZone"))
        {
            gameManager.WinGame();
        }
    }

}
