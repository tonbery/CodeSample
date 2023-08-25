using System.Collections.Generic;

[System.Serializable]
public static class SkillEffect
{
    public static void Activate(BattleCharacter caster, List<BattleCharacter> targets, SkillEffectData data)
    {
        switch (data.SkillEffectType())
        {
              case ESkillEffect.Cure: SE_Cure.Activate(caster, targets, data); break;
              case ESkillEffect.Damage: SE_Damage.Activate(caster, targets, data); break;
              case ESkillEffect.PlaySound: SE_PlaySound.Activate(caster, targets, data); break;
        }
    }
}