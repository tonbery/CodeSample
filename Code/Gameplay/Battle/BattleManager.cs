using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    
    [Header("Debug")]
    [SerializeField] private EncounterData debugEncounter;
    private RuntimeEncounter _currentEncounter;

    [Header("Project References")] 
    [SerializeField] private GameObject characterPrefab;
    
    [Header("SceneReferences")]
    [SerializeField] private GameObject[] playerPoints;
    [SerializeField] private GameObject[] enemyPoints;
    [SerializeField] private GameObject outsidePoint;
    
    [Header("Battle Setup")]
    [SerializeField] private float deathPauseTime = 1;

    
    private BattleCharacter _selectedCharacter;
    private List<BattleCharacter> _playerCharacters;
    private List<BattleCharacter> _enemyCharacters;
    private int _enemyIndexOnSquad;
    private int _maxEnemyCount;
    private int _squadIndex;

    private bool _lockSpawn;
    
    public UnityEvent<BattleCharacter> OnCharacterSpawn = new();
    public UnityEvent OnSquandChanged = new();

    public List<BattleCharacter> PlayerCharacters => _playerCharacters;
    public List<BattleCharacter> EnemyCharacters => _enemyCharacters;

    public UnityEvent OnVictory = new ();
    public UnityEvent OnDefeat = new ();

    private bool ended;
    private bool victory;
    
    private SquadData CurrentSquad => _currentEncounter.Squads[_squadIndex];
    private bool HaveCurrentSquad => _squadIndex > -1 && _squadIndex < _currentEncounter.Squads.Count - 1;

    public bool Ended => ended;
    public bool Victory => victory;

    public int MaxEnemyCount => _maxEnemyCount;

    public GameObject[] EnemyPoints => enemyPoints;

    private Coroutine _playerDeathCoroutine;
    

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        _playerCharacters = new List<BattleCharacter>();
        _enemyCharacters = new List<BattleCharacter>();

        var playerSheets = GameInstance.GetCurrentSave().currentParty;
        for (int i = 0; i < playerSheets.Count; i++)
        {
            var newPlayerCharacter = SpawnCharacter(playerSheets[i], playerPoints[i], i, i, 0, false);
            _playerCharacters.Add(newPlayerCharacter);
            newPlayerCharacter.OnDeath.AddListener(OnPlayerDeath);
        }

        if (GameInstance.RuntimeEncounter.IsSet) _currentEncounter = GameInstance.RuntimeEncounter;
        else _currentEncounter = new RuntimeEncounter(debugEncounter);

        _squadIndex = -1;
        AdvanceFlow();
       
        SelectCharacter(0);
    }

    private void OnPlayerDeath(BattleCharacter playerCharacter)
    {
        if(_playerDeathCoroutine != null) StopCoroutine(_playerDeathCoroutine);
        _playerDeathCoroutine = StartCoroutine(OnPlayerCharacterDeathRoutine(playerCharacter));
    }

    

    void SpawnEnemy()
    {
        var sheet = CurrentSquad.Enemies[_enemyIndexOnSquad];
        var pointIndex = _enemyCharacters.Count-1;
        var newEnemy = SpawnCharacter( sheet, outsidePoint, pointIndex, pointIndex, 1, true);
        _enemyCharacters.Add(newEnemy);
        newEnemy.OnDeath.AddListener(OnEnemyDeath);
        _enemyIndexOnSquad++;
        
        _enemyCharacters[^1].SetPositionTarget(enemyPoints[_enemyCharacters.Count-1], _enemyCharacters.Count-1);
    }

    void AdvanceFlow()
    {
        if (_enemyCharacters.Count == 0) _lockSpawn = false;
        
        if (_squadIndex != _currentEncounter.Squads.Count -1 && (_squadIndex == -1 || _enemyIndexOnSquad >= CurrentSquad.Enemies.Count))
        {
            bool canIncrement = true;

            if (HaveCurrentSquad && CurrentSquad.BlockNextSquad && _enemyCharacters.Count > 0) canIncrement = false;

            if (canIncrement)
            {
                _squadIndex++;
                _enemyIndexOnSquad = 0;
                _maxEnemyCount = CurrentSquad.MaxEnemyCount;
                OnSquandChanged.Invoke();
                if (CurrentSquad.WaitCurrentSquadEnd && _squadIndex > 0) _lockSpawn = true;
            }
        }

        while (!_lockSpawn && _enemyCharacters.Count < _maxEnemyCount && _enemyIndexOnSquad < CurrentSquad.Enemies.Count)
        {
            SpawnEnemy();
        }

        Reorder();
    }

    void Reorder()
    {
        for (var i = 0; i < _enemyCharacters.Count; i++)
        {
            var enemyCharacter = _enemyCharacters[i];
            enemyCharacter.SetPositionTarget(enemyPoints[i], i);
        }
    }
    private void OnEnemyDeath(BattleCharacter enemy)
    {
        _enemyCharacters.Remove(enemy);

        if (_squadIndex == _currentEncounter.Squads.Count-1 && _enemyCharacters.Count == 0)
        {
           OnVictory.Invoke();
           ended = true;
           victory = true;
            return;
        }

        if (_squadIndex == _currentEncounter.Squads.Count - 1 && _enemyIndexOnSquad == CurrentSquad.Enemies.Count - 1)
        {
            Reorder();
            return;
        }

        AdvanceFlow();
    }

    BattleCharacter SpawnCharacter(CharacterSheet sheet, GameObject point, int characterIndex, int pointIndex, int team, bool isAI)
    {
        var newCharacter = Instantiate(characterPrefab, point.transform.position, Quaternion.identity).GetComponent<BattleCharacter>();
        newCharacter.SetCharacter(sheet, characterIndex, isAI, team);
        newCharacter.SetPositionTarget(point, pointIndex);
        OnCharacterSpawn.Invoke(newCharacter);
        return newCharacter;
    }
    
    public void SetCharacterInput(int characterIndex, bool state)
    {
        if (BattleTime.IsOnPause) return;
        if (characterIndex >= _playerCharacters.Count) return;
        if (!_playerCharacters[characterIndex].Alive) return;
        
        if (state)
        {
            if(_selectedCharacter && _playerCharacters[characterIndex] != _selectedCharacter) _selectedCharacter.StopCharge(false);
            SelectCharacter(characterIndex);
            
            _selectedCharacter.StartCharge();
        }
        else if(_playerCharacters[characterIndex] == _selectedCharacter)
        {
            _playerCharacters[characterIndex].StopCharge();
        }
    }

    void SelectCharacter(int characterIndex)
    {
        SelectCharacter(_playerCharacters[characterIndex]);
    }

    void SelectCharacter(BattleCharacter character)
    {
        if (_selectedCharacter == character) return;

        if (_selectedCharacter)
        {
            var oldPoint = _selectedCharacter.CurrentPositionTarget;
            var oldPointIndex = _selectedCharacter.CurrentPointIndex;
            
            _selectedCharacter.UnselectCharacter();
            _selectedCharacter.SetPositionTarget(character.CurrentPositionTarget, character.CurrentPointIndex);
           
            character.SelectCharacter();
            character.SetPositionTarget(oldPoint, oldPointIndex);
        }
            
        character.SelectCharacter();
        _selectedCharacter = character;
    }

    public List<BattleCharacter> GetOppositeTeam(BattleCharacter character)
    {
        if (character.Team == 0) return _enemyCharacters;
        return _playerCharacters;
    }
    
    public List<BattleCharacter> GetTeam(BattleCharacter character)
    {
        if (character.Team == 0) return _playerCharacters;
        return _enemyCharacters;
    }

    IEnumerator OnPlayerCharacterDeathRoutine(BattleCharacter playerCharacter)
    {
        BattleTime.Pause();

        bool isDefeat = true;
        
        foreach (var character in _playerCharacters)
        {
            if (character.Alive)
            {
                isDefeat = false;
                break;
            }
        }

        if (!isDefeat && _selectedCharacter == playerCharacter)
        {
            float bestHP = 0;
            BattleCharacter bestCharacter = null;
            
            foreach (var character in _playerCharacters)
            {
                var charHP = character.Attributes.GetValue(EAttributes.CurrentHealth);

                if (charHP > bestHP)
                {
                    bestCharacter = character;
                    bestHP = charHP;
                }
            }
            
            yield return new WaitForSeconds(deathPauseTime);
            
            SelectCharacter(bestCharacter);
        }

        if (isDefeat)
        {
            yield return new WaitForSeconds(deathPauseTime + 3);
            OnDefeat.Invoke();
            ended = true;
            victory = false;    
        }
        else
        {
            BattleTime.UnPause();
        }
    }
}
