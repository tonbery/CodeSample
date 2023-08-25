using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;


public struct RuntimeEncounter
{
    public bool IsSet;
    public List<SquadData> Squads;

    public RuntimeEncounter(EncounterData data)
    {
        IsSet = true;
        Squads = new List<SquadData>();

        var targetSquadCount = data.SquadCount.GetRandomValueI(); 
        var squadCount = 0;

        List<EncounterSquadData> squadBag = new();
        List<EncounterSquadData> waitingSquads = new();

        while (squadCount < targetSquadCount)
        {
            bool foundRequired = false;

            List<SquadData> guaranteedSquads = new(); 
                
            foreach (var squadData in data.Squads)
            {
                if (squadData.guaranteedIndex == squadCount)
                {
                    guaranteedSquads.Add(squadData.squadData);
                }
            }

            if (guaranteedSquads.Count > 0)
            {
                Squads.Add(guaranteedSquads[Random.Range(0,guaranteedSquads.Count)]);
                squadCount++;
                foundRequired = true;
            }
            
            if(foundRequired) continue;

            if (squadBag.Count == 0)
            {
                for (int i = 0; i < data.Squads.Count; i++)
                {
                    for (int j = 0; j < data.Squads[i].amountInBag; j++)
                    {
                        if (data.Squads[i].minIndex <= squadCount && data.Squads[i].maxIndex >= squadCount)
                        {
                            squadBag.Add(data.Squads[i]);
                        }
                        else if(data.Squads[i].maxIndex > squadCount)
                        {
                            waitingSquads.Add(data.Squads[i]);
                        }
                    }
                }
            }
            else
            {
                for (int i = squadBag.Count - 1; i >= 0; i--)
                {
                    if (squadBag[i].maxIndex >= squadCount)
                    {
                        squadBag.RemoveAt(i);
                    }
                }
                
                if(squadBag.Count == 0) continue;
            }

            var squadIndex = Random.Range(0, squadBag.Count);
            var squad = squadBag[squadIndex];

            squadBag.RemoveAt(squadIndex);
            Squads.Add(squad.squadData);
            squadCount++;

            for (int i = waitingSquads.Count - 1; i >= 0; i--)
            {
                if (waitingSquads[i].minIndex <= squadCount)
                {
                    for (int j = 0; j < waitingSquads[i].amountInBag; j++)
                    {
                        squadBag.Add(waitingSquads[i]);
                    }
                    waitingSquads.RemoveAt(i);
                }
            }
        }
    }
}


[System.Serializable]
public class EncounterSquadData
{
    [SerializeField] public int amountInBag = 1;
    [SerializeField] public int minIndex = 0;
    [SerializeField] public int maxIndex = 999;
    [SerializeField] public int guaranteedIndex = -1;
    [SerializeField] public SquadData squadData;
} 

[CreateAssetMenu(fileName = "EncounterData", menuName = "BRPG/Data/Encounter")]
public class EncounterData : ScriptableObject
{
    [SerializeField] private MinMax squadCount;
    [SerializeField] private List<EncounterSquadData> squads;
    public List<EncounterSquadData> Squads => squads;

    public MinMax SquadCount => squadCount;

    public RuntimeEncounter MakeRuntimeEncounter()
    {
        return new RuntimeEncounter(this);
    }
    
    
}
