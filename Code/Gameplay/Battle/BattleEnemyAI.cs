using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleEnemyAI : MonoBehaviour
{
   private BattleCharacter _character;
   private float _currentChargeTime;
   private float _targetChargeTime = 9999;
   private SkillSlot _currentSkill; 
   
   public void SetCharacter(BattleCharacter character)
   {
      _character = character;
      ChooseRandomSkill();
      _targetChargeTime += Random.Range(_targetChargeTime / 4, _targetChargeTime / 2);
   }

   void ChooseRandomSkill()
   {
      _currentSkill = _character.GetRandomSkillSlot();
      _targetChargeTime = _currentSkill.chargeTime;
      _currentChargeTime = 0;
   }

   private void Update()
   {
      _currentChargeTime += BattleTime.DeltaTime;
      
      if (_currentChargeTime >= _targetChargeTime)
      {
         _character.SkillManager.CastSkill(_currentSkill.skill);
         ChooseRandomSkill();
         
      }
   }
}
