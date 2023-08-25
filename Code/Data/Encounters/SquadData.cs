using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SquadData", menuName = "BRPG/Data/Squad")]

public class SquadData : ScriptableObject
{
    [SerializeField] private int maxEnemyCount;
    [SerializeField] List<CharacterSheet> enemies;
    [SerializeField] private bool waitCurrentSquadEnd;
    [SerializeField] private bool blockNextSquad;

    public int MaxEnemyCount => maxEnemyCount;
    public List<CharacterSheet> Enemies => enemies;
    public bool WaitCurrentSquadEnd => waitCurrentSquadEnd;
    public bool BlockNextSquad => blockNextSquad;
}
