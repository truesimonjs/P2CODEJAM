using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandOffice : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Vector2 screenPosition = Vector2.zero;

        // Check for touch input
        if (Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position;
        }
        // Check for mouse input
        /*
        else if (Input.GetMouseButton(0))
        {
            screenPosition = Input.mousePosition;
        }
        */
        else
        {
            return; // Exit if no input detected
        }

        // Convert screen position to a world space ray
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Keep Y position unchanged
            rb.MovePosition(targetPosition);
        }
    }
}