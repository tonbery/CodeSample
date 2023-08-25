using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBarController : MonoBehaviour
{
    [SerializeField] private CharacterBar smallBar;
    [SerializeField] private CharacterBar bigBar;
    [SerializeField] private bool isSelected;
    [SerializeField] private InputUI characterInputButtonUI;
    
    
    public InputUI CharacterInputButtonUI => characterInputButtonUI;
    private void OnValidate()
    {
        UpdateBarState();
    }

    public void SetCharacter(BattleCharacter character)
    {
        smallBar.SetCharacter(character);
        bigBar.SetCharacter(character);
        character.OnSelectChange.AddListener(OnSelectChange);
        character.OnDeath.AddListener(OnDeath);

        isSelected = character.IsSelected;
    }

    private void OnDeath(BattleCharacter arg0)
    {
        gameObject.SetActive(false);
    }

    private void OnSelectChange(bool state)
    {
        isSelected = state;
        UpdateBarState();
    }

    void UpdateBarState()
    {
        if(smallBar) smallBar.gameObject.SetActive(!isSelected);
        if(bigBar) bigBar.gameObject.SetActive(isSelected);
    }
}
