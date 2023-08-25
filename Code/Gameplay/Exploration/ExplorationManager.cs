using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

struct BattleAreaRuntimeData
{
    bool _inBattleArea;
    BattleAreaData _battleAreaData;
    float _timeInBattleArea;
    float _timeToEncounter;

    public void EnterArea(BattleArea area)
    {
        _battleAreaData = area.BattleAreaData;
        _inBattleArea = true;
        _timeInBattleArea = 0;
        CalculateRandomTime();
    }
    void CalculateRandomTime()
    {
        _timeToEncounter =  Random.Range(_battleAreaData.minTime, _battleAreaData.maxTime);
    }
    public bool NeedsToBattle()
    {
        if (!_inBattleArea) return false;

        return _timeInBattleArea > _timeToEncounter;
    }

    public void DebugTime()
    {
        Debug.Log(_timeToEncounter - _timeInBattleArea);
    }
    public void ExitBattleArea()
    {
        _inBattleArea = false;
    }

    public EncounterData GetEncounter()
    {
        Dictionary<float, EncounterData> encounters = new ();

        float sum = 0;
        foreach (var encounter in _battleAreaData.encounters)
        {
            sum += encounter.chance;
            encounters.Add(sum, encounter.encounter);
        }

        float random = Random.Range(0, sum);

        foreach (var encounter in encounters)
        {
            if (random <= encounter.Key)
            {
                Debug.Log(encounter.Value.name);
                return encounter.Value;
            }
        }
        
        return null;
    }

    public void DecreaseTime()
    {
        _timeInBattleArea += Time.deltaTime;
    }
}
public class ExplorationManager : MonoBehaviour
{
    private static ExplorationManager _instance;
    public static ExplorationManager Instance => _instance;

    public ExplorationPlayerCharacter PlayerCharacter => _playerCharacter;

    [SerializeField] private ExplorationPlayerCharacter explorationPlayerCharacterPrefab;
    [SerializeField] private ExplorationInput input;
    
    [SerializeField] private CinemachineVirtualCamera mainVirtualCamera;

    [SerializeField] private PlayerSpawnPoint defaultSpawnPoint;

    [SerializeField] private List<TeleportArea> teleports;
    [SerializeField] private List<BattleArea> battleAreas;

    private BattleAreaRuntimeData _battleAreaData;

    private ExplorationPlayerCharacter _playerCharacter;

   

    private void Awake()
    {
        _instance = this;
        
        _playerCharacter = Instantiate(explorationPlayerCharacterPrefab);

        SetPlayerPosition();
        
        input.Initialize(_playerCharacter, this);
        _playerCharacter.Initialize();

        mainVirtualCamera.Follow = _playerCharacter.transform;

        foreach (var battleArea in battleAreas)
        {
            battleArea.onPlayerEnterArea.AddListener(OnPlayerEnterBattleArea);
            battleArea.onPlayerExitArea.AddListener(OnPlayerExitBattleArea);
        }
    }

    private void OnPlayerExitBattleArea(BattleArea battleArea)
    {
        _battleAreaData.ExitBattleArea();
    }

    private void OnPlayerEnterBattleArea(BattleArea battleArea)
    {
        _battleAreaData.EnterArea(battleArea);
    }

    void SetPlayerPosition()
    {
        if (GameInstance.CameFromBattle)
        {
            _playerCharacter.transform.position = GameInstance.LastPlayerPosition;
            return;
        }
        if (GameInstance.NextSpawnPoint != String.Empty)
        {
            foreach (var teleport in teleports)
            {
                if (teleport.name == GameInstance.NextSpawnPoint)
                {
                    _playerCharacter.transform.position = teleport.SpawnPoint.transform.position;
                    return;
                }
            }
        }
        
        _playerCharacter.transform.position = defaultSpawnPoint.transform.position;
    }

    [Button]
    void ScanLevel()
    {
        var foundTeleports =  FindObjectsByType<TeleportArea>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        List<string> teleportNames = new List<string>();
        teleports = new List<TeleportArea>();
        foreach (var teleport in foundTeleports)
        {
            teleports.Add(teleport);
            teleportNames.Add(teleport.name);
        }
        
        var foundBattleAreas =  FindObjectsByType<BattleArea>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        battleAreas = new List<BattleArea>();
        foreach (var battleArea in foundBattleAreas)
        {
            battleAreas.Add(battleArea);
        }

        GameTags.GetInstance().SetTeleportTags(SceneManager.GetActiveScene().name, teleportNames);
        
        EditorUtility.SetDirty(this);
    }

    private void Update()
    {
       

        if (_playerCharacter.CurrentSpeed > 0.1f)
        {
            //_battleAreaData.DebugTime();
            _battleAreaData.DecreaseTime();
        }
        
        if (_battleAreaData.NeedsToBattle())
        {
            GameInstance.LoadBattle(_battleAreaData.GetEncounter().MakeRuntimeEncounter(), _playerCharacter.transform.position);
        }
    }
}
