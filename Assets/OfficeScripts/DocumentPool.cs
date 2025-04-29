using UnityEngine;
using System.Collections.Generic;

public class DocumentPool : MonoBehaviour
{
    public Queue<GameObject> pool = new Queue<GameObject>();

    public void AddToPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        pool.Enqueue(gameObject);
        Debug.Log("Added to pool");
    }

    public GameObject GetFromPool()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            
            // Reset velocity if Rigidbody is present
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            
            return obj;
        }
        else
        {
            Debug.LogWarning("Pool is empty!");
            return null;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
