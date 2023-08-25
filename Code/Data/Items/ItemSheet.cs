using UnityEngine;

[System.Serializable]
public class ItemSheet : ScriptableObject
{
    public enum EItemType
    {
        Consumable,
        Key,
        Collectible,
        Equipment,
    }

    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite itemName;
    [SerializeField] private Sprite itemDescription;

    public virtual EItemType ItemType() => EItemType.Consumable;
}