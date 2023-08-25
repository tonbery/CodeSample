using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleExecutionController : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private BattleInput battleInput;
    [SerializeField] private BattleHUD battleHUD;
    [SerializeField] private BattleCameraController battleCamera;
    void Awake()
    {
        battleManager.Initialize();
        battleInput.Initialize();
        battleHUD.Initialize();
        battleCamera.Initialize(battleManager);
    }

   
}
