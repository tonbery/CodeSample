using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[System.Serializable]
public static class SE_Cure
{
    public static void Activate(BattleCharacter caster, List<BattleCharacter> battleCharacters, SkillEffectData data)
    {
        var cureData = (SE_Cure_Data)data;
        float rawCure = (caster.Attributes.GetValue(EAttributes.Power) / 100f) * cureData.skillPower * Random.Range(caster.Attributes.GetValue(EAttributes.RandomMin), caster.Attributes.GetValue(EAttributes.MaxHealth));
        
        foreach (var target in battleCharacters)
        {
            target.Cure(rawCure);
        }
    }
}
