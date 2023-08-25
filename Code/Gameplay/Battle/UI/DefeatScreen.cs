using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatScreen : BattleResultSreen
{
    protected override void Start()
    {
        base.Start();
        
        if (BattleManager.Instance.Ended)
        {
            if (!BattleManager.Instance.Victory)
            {
                Open();
            }
        }
        else
        {
            BattleManager.Instance.OnDefeat.AddListener(Open);
        }
    }

    protected override void OkPressed()
    {
        GameInstance.BackToExploration();
    }

}
