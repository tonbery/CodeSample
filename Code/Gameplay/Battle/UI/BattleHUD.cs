using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct BarTransform
{
    public Vector2 Position;
}
public class BattleHUD : MonoBehaviour
{
    [SerializeField] private Sprite[] inputSprites;
    
    [SerializeField] private Transform runtimeUI;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private CharacterBarController[] chargeBars;

    [SerializeField] private GameObject introUI;
    [SerializeField] private GameObject battleUI;
    [SerializeField] private GameObject endGameUI;
    
    [SerializeField] private float _showUIDelay = 2; 
    
    private List<BarTransform> _originalBarTransforms;

    private BattleManager _battleManager;

    [SerializeField] private bool showIntro = true;

    private IEnumerator Start()
    {
        if (showIntro)
        {
            endGameUI.SetActive(false);
            introUI.SetActive(true);
            battleUI.SetActive(false);
        
            yield return new WaitForSeconds(_showUIDelay);
            introUI.SetActive(false);
            battleUI.SetActive(true);
        }
        else
        {
            endGameUI.SetActive(false);
            introUI.SetActive(false);
            battleUI.SetActive(true);
        }
    }

    public void Initialize()
    {
        _originalBarTransforms = new List<BarTransform>();
        
        _battleManager = FindObjectOfType<BattleManager>();
        
        for (int i = 0; i < _battleManager.PlayerCharacters.Count; i++)
        {
            chargeBars[i].SetCharacter(_battleManager.PlayerCharacters[i]);
          
            var barTransform = new BarTransform();
            var originalBarTransform = (RectTransform)chargeBars[i].transform;
            barTransform.Position = originalBarTransform.anchoredPosition;
            _originalBarTransforms.Add(barTransform);
            _battleManager.PlayerCharacters[i].OnChangePoint.AddListener(OnCharacterChange);
            UpdateInput(_battleManager.PlayerCharacters[i]);
        }

        /*foreach (var playerCharacter in _battleManager.PlayerCharacters)
        {
            CreateHealthBar(playerCharacter);
        }*/
        
        foreach (var enemyCharacter in _battleManager.EnemyCharacters)
        {
            CreateHealthBar(enemyCharacter);
        }
        
        _battleManager.OnCharacterSpawn.AddListener(CreateHealthBar);
        
        BattleManager.Instance.OnVictory.AddListener(OnEnd);
        BattleManager.Instance.OnDefeat.AddListener(OnEnd);
    }

    private void OnEnd()
    {
        Debug.Log("end");
        endGameUI.SetActive(true);
        introUI.SetActive(false);
        battleUI.SetActive(false);
    }

    void CreateHealthBar(BattleCharacter character)
    {
        var newBar = Instantiate(healthBarPrefab);
        newBar.GetComponent<HealthBar>().SetCharacter(character);
        runtimeUI.AddChild(newBar);
        newBar.transform.localScale = Vector3.one;
        
    }

    private void OnCharacterChange(BattleCharacter character)
    {
        var barTransform = (RectTransform)chargeBars[character.CharacterIndex].transform;
        barTransform.anchoredPosition = _originalBarTransforms[character.CurrentPointIndex].Position;
        //barTransform.localScale = Vector3.one * (character.IsSelected ? 1 : 0.5f);
        UpdateInput(character);
    }

    public void UpdateInput(BattleCharacter character)
    {
        chargeBars[character.CharacterIndex].CharacterInputButtonUI.SetKeySprite(inputSprites[character.CharacterIndex]);
    }
}
