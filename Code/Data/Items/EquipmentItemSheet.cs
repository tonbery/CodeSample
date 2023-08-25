using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EquipmentItem", menuName = "BRPG/Item/Equipment")]
public class EquipmentItemSheet : ItemSheet
{
    public override EItemType ItemType() => EItemType.Equipment;
}