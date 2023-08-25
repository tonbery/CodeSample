using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct BattleEncounterData
{
    public float chance;
    public EncounterData encounter;
}

[System.Serializable]
public struct BattleAreaData
{
    public float minTime;
    public float maxTime;
    public List<BattleEncounterData> encounters;
}

public class BattleArea : MonoBehaviour
{
    [SerializeField] private BattleAreaData battleAreaData;
    public UnityEvent<BattleArea> onPlayerEnterArea = new ();
    public UnityEvent<BattleArea> onPlayerExitArea = new ();

    public BattleAreaData BattleAreaData => battleAreaData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerEnterArea.Invoke(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerExitArea.Invoke(this);
        }
    }
}
