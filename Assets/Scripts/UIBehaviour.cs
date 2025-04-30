using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Button closeButton;
    public float swipeThreshold = 100f; // Drag distance before closing
    private Vector3 startPos;
    private Canvas canvas;

    void Start()
    {
        // Find and assign close button
        if (closeButton == null)
            closeButton = GetComponentInChildren<Button>();

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseUI);

        // Store starting position
        startPos = transform.localPosition;

        // Get parent canvas (for scaling drag)
        canvas = GetComponentInParent<Canvas>();
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Nothing special here, but needed for IDragHandler
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert screen drag to world drag (for world space canvas)
        Vector3 dragPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out dragPos);

        transform.position = dragPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check if swipe distance is enough
        float dragDistance = Vector3.Distance(transform.localPosition, startPos);

        if (dragDistance > swipeThreshold)
        {
            // Swipe passed threshold ➡️ close
            CloseUI();
        }
        else
        {
            // Not enough swipe ➡️ snap back
            StartCoroutine(SnapBack());
        }
    }

    IEnumerator SnapBack()
    {
        float elapsed = 0f;
        float duration = 0.2f;
        Vector3 currentPos = transform.localPosition;

        while (elapsed < duration)
        {
            transform.localPosition = Vector3.Lerp(currentPos, startPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPos;
    }
}
