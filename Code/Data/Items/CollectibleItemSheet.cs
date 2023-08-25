using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "CollectibleItem", menuName = "BRPG/Item/Collectible")]
public class CollectibleItemSheet : ItemSheet
{
    public override EItemType ItemType() => EItemType.Collectible;
}