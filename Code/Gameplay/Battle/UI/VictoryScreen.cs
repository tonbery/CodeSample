using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : BattleResultSreen
{
    protected override void Start()
    {
        base.Start();
        
        if (BattleManager.Instance.Ended)
        {
            if (BattleManager.Instance.Victory)
            {
                Open();
            }
        }
        else
        {
            BattleManager.Instance.OnVictory.AddListener(Open);
        }
    }

    protected override void OkPressed()
    {
        
        GameInstance.BackToExploration();
    }
}
