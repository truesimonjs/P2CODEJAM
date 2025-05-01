using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class SwipeableUI : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private RectTransform rectTransform;
    private bool isTouching = false;
    private bool isMyTouch = false; // Flag to track if it's the correct UI being dragged
    private static SwipeableUI currentUI; // Track which UI element is currently being swiped

    public float swipeSpeed = 0.5f;
    public float swipeThreshold = 10f;

    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        raycaster = FindFirstObjectByType<GraphicRaycaster>();
        eventSystem = FindFirstObjectByType<EventSystem>();
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                // Only start dragging if touch is on this specific UI element
                if (IsTouchOnThisUI(touchPos))
                {
                    isTouching = true;
                    isMyTouch = true; // This UI is the one being interacted with
                    startTouchPosition = touchPos;
                    currentUI = this; // Set this UI as the active one being dragged
                }
                else
                {
                    isMyTouch = false; // Don't start dragging
                }
            }

            if (isTouching && isMyTouch && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {
                endTouchPosition = touchPos;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
                isMyTouch = false;
                currentUI = null; // Reset after touch ends
            }
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Only start dragging if mouse is on this specific UI element
            if (IsTouchOnThisUI(Input.mousePosition))
            {
                isTouching = true;
                isMyTouch = true; // This UI is the one being interacted with
                startTouchPosition = Input.mousePosition;
                currentUI = this; // Set this UI as the active one being dragged
            }
            else
            {
                isMyTouch = false; // Don't start dragging
            }
        }
        else if (Input.GetMouseButton(0) && isTouching && isMyTouch)
        {
            endTouchPosition = Input.mousePosition;
            DetectSwipe();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isTouching = false;
            isMyTouch = false;
            currentUI = null; // Reset after mouse button is released
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
            // Only return true if the touch/click is on this specific UI element
            if (result.gameObject == gameObject || result.gameObject.transform.IsChildOf(transform))
                return true;
        }

        return false;
    }

    void DetectSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        if (swipeDelta.magnitude > swipeThreshold)
        {
            // Move the UI element only if it's the active one being interacted with
            Vector3 newPos = rectTransform.anchoredPosition + swipeDelta * swipeSpeed;
            rectTransform.anchoredPosition = newPos;

            startTouchPosition = endTouchPosition;
        }
    }
}
