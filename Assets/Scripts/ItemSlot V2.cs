using UnityEngine;
using UnityEngine.EventSystems;


public class ItemSlotV2 : MonoBehaviour, IDropHandler
{
    public ItemType acceptedType;
    public OutfitManager outfitManager;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        var item = eventData.pointerDrag?.GetComponent<ClothingItem>();
        if (item != null && item.itemType == acceptedType)
        {
            item.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            outfitManager.EquipItem(acceptedType, item);
        }
    }
}