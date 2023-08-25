using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RewardData 
{
    [SerializeField] private List<ItemSlot> items;
    public List<ItemSlot> Items => items;
}


[CreateAssetMenu(menuName = "BRPG/RewardData", fileName = "RewardDataAsset", order = 0)]
public class RewardDataAsset: ScriptableObject
{
    [SerializeField] private RewardData rewardData;

    public RewardData Data => rewardData;
}