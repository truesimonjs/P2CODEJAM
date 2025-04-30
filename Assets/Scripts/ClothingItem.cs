using UnityEngine;

public enum ItemType { HeadSlot, TorsoSlot, LegSlot, FeetSlot }

public class ClothingItem : MonoBehaviour
{
    public ItemType itemType;
    public int stylePoints;
}
