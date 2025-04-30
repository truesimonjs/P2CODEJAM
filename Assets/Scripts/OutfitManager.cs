using System.Collections.Generic;
using UnityEngine;

public class OutfitManager : MonoBehaviour
{
    private Dictionary<ItemType, ClothingItem> equippedItems = new Dictionary<ItemType, ClothingItem>();

    public void EquipItem(ItemType type, ClothingItem item)
    {
        equippedItems[type] = item;
        Debug.Log($"{item.name} equipped in {type}");
    }

    public void UnequipItem(ItemType type)
    {
        if (equippedItems.ContainsKey(type))
        {
            Debug.Log($"{equippedItems[type].name} unequipped from {type}");
            equippedItems.Remove(type);
        }
    }

    public Dictionary<ItemType, ClothingItem> GetEquippedItems()
    {
        return equippedItems;
    }
}
