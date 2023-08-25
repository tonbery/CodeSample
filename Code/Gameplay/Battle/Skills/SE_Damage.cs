using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class SE_Damage
{
    public static void Activate(BattleCharacter caster, List<BattleCharacter> battleCharacters, SkillEffectData data)
    {
        var effectData = (TargetedEffectData)data;
        float rawDamage = (caster.Attributes.GetValue(EAttributes.Power) / 100f) * effectData.skillPower * Random.Range(caster.Attributes.GetValue(EAttributes.RandomMin), caster.Attributes.GetValue(EAttributes.MaxHealth));
        
        foreach (var target in battleCharacters)
        {
            target.TakeDamage(rawDamage);
        }
    }
}
