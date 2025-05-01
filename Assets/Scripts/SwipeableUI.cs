using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class SwipeableUI : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private RectTransform rectTransform;
    private bool isTouching = false;

    public float swipeSpeed = 0.1f;
    public float swipeThreshold = 100f;

    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;
            Debug.Log("1");

            if (touch.phase == TouchPhase.Began && IsTouchOnThisUI(touchPos))
            {
                isTouching = true;
                startTouchPosition = touchPos;
                Debug.Log("2");
            }

            if (touch.phase == TouchPhase.Moved && isTouching)
            {
                endTouchPosition = touchPos;
                DetectSwipe();
                Debug.Log("3");
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
                Debug.Log("4");
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            if (IsTouchOnThisUI(mousePos))
            {
                isTouching = true;
                startTouchPosition = mousePos;
                Debug.Log("5");
            }
        }
        else if (Input.GetMouseButton(0) && isTouching)
        {
            endTouchPosition = Input.mousePosition;
            DetectSwipe();
            Debug.Log("6");
        }

        if (Input.GetMouseButtonUp(0))
        {
            isTouching = false;
            Debug.Log("7");
        }
    }

    bool IsTouchOnThisUI(Vector2 position)
    {
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject == gameObject)
                return true;
        }

        return false;
    }

    void DetectSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        if (swipeDelta.magnitude > swipeThreshold)
        {
            Vector3 newPos = rectTransform.anchoredPosition + new Vector2(swipeDelta.x * swipeSpeed, 0);
            rectTransform.anchoredPosition = newPos;
            startTouchPosition = endTouchPosition;
        }
    }
}
