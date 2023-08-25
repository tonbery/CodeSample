using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExplorationInput : MonoBehaviour
{
    private ExplorationPlayerCharacter _explorationPlayerCharacter;
    private ExplorationManager _manager;
    private PlayerInputActions _playerInputActions;

    private Vector2 _movementAxis;
    
    public void Initialize(ExplorationPlayerCharacter character, ExplorationManager manager)
    {
        _explorationPlayerCharacter = character;
        _manager = manager;
        
        RegisterInputs();
    }

    void RegisterInputs()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Exploration.Enable();
        
        _playerInputActions.Exploration.Movement.performed += MovementOnPerformed;
        _playerInputActions.Exploration.Movement.canceled += MovementOnPerformed;
        _playerInputActions.Exploration.Interaction.performed += Interact;
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        if (_explorationPlayerCharacter.Interaction.CurrentInteraction)
        {
            _explorationPlayerCharacter.Interaction.CurrentInteraction.OnSelectUsable();
            _explorationPlayerCharacter.Interaction.CurrentInteraction.OnUseUsable(); 
            _explorationPlayerCharacter.Interaction.CurrentInteraction.gameObject.BroadcastMessage("OnUse", transform, SendMessageOptions.DontRequireReceiver);
            Debug.Log("interact");    
        }

        
    }


    private void MovementOnPerformed(InputAction.CallbackContext context)
    {
        _movementAxis = context.ReadValue<Vector2>();
        _explorationPlayerCharacter.SetMovement(_movementAxis);
    }


    private void OnDestroy()
    {
        _playerInputActions.Exploration.Disable();
    }
}
