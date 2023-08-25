using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ConsumableItem", menuName = "BRPG/Item/Consumable")]
public class ConsumableItemSheet : ItemSheet
{
    public override EItemType ItemType() => EItemType.Consumable;
}