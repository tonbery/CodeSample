using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[System.Serializable]
public class MainSave
{
    [SerializeField] public string saveName;
    [SerializeField] public List<CharacterSave> characters;
    [SerializeField] public List<CharacterSheet> currentParty;
    [SerializeField] public List<ItemSlot> items;

    public void PickItem(RewardData reward)
    {
        foreach (var item in reward.Items)
        {
            AddItem(item);
        }
    }
    
    public void PickItem(RewardDataAsset reward)
    {
        PickItem(reward.Data);
    }

    void AddItem(ItemSlot itemSlot)
    {
        foreach (var item in items)
        {
            if (item.item == itemSlot.item)
            {
                item.count += itemSlot.count;
                return;
            }
        }
        
        items.Add(itemSlot);
    }
}
