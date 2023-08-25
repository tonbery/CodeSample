using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "KeyItem", menuName = "BRPG/Item/Key")]
public class KeyItemSheet : ItemSheet
{
    public override EItemType ItemType() => EItemType.Key;
}