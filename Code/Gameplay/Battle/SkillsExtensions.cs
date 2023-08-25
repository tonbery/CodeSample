using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillsExtensions
{
    public static SkillData GetRandomSkill(this CharacterSheet character)
    {
        return GetRandomSkillSlot(character).skill;
    }
    public static SkillData GetRandomSkill(this BattleCharacter character)
    {
        return GetRandomSkill(character.Sheet);
    }
    
    public static SkillSlot GetRandomSkillSlot(this CharacterSheet character)
    {
        var count = character.Skills.Count;
        return character.Skills[Random.Range(0, count)];
    }
    public static SkillSlot GetRandomSkillSlot(this BattleCharacter character)
    {
        return GetRandomSkillSlot(character.Sheet);
    }
}
