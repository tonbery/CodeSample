using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleInput : MonoBehaviour
{
    private BattleManager _battleManager;
    private PlayerInputActions _playerInputActions;
    
    public void Initialize()
    {
        _battleManager = GetComponent<BattleManager>();
        RegisterInputs();
    }
    
    void RegisterInputs()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Battle.Enable();
        _playerInputActions.Battle.Character1.started += delegate(InputAction.CallbackContext context)
        {
            _battleManager.SetCharacterInput(0, true);  
        };
        _playerInputActions.Battle.Character1.canceled += delegate(InputAction.CallbackContext context)
        {
            _battleManager.SetCharacterInput(0, false);  
        };
        _playerInputActions.Battle.Character2.started += delegate(InputAction.CallbackContext context)
        {
            _battleManager.SetCharacterInput(1, true);  
        };
        _playerInputActions.Battle.Character2.canceled += delegate(InputAction.CallbackContext context)
        {
            _battleManager.SetCharacterInput(1, false);  
        };
        _playerInputActions.Battle.Character3.started += delegate(InputAction.CallbackContext context)
        {
            _battleManager.SetCharacterInput(2, true);  
        };
        _playerInputActions.Battle.Character3.canceled += delegate(InputAction.CallbackContext context)
        {
            _battleManager.SetCharacterInput(2, false);  
        };
    }

    

    private void OnDestroy()
    {
        _playerInputActions.Battle.Disable();
    }
}
