using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillManager
{
    private BattleCharacter _caster;
    
    private Dictionary<int, List<SkillSlot>> _allSkills = new ();
    private Dictionary<int, List<SkillData>> _shuffleBags = new ();

    public UnityEvent<SkillData> OnUseSkill = new();
    public UnityEvent<int> OnSkillSlotChanged = new();

    public Dictionary<int, List<SkillData>> ShuffleBags => _shuffleBags;

    public void SetCharacter(BattleCharacter newCharacter)
    {
        _caster = newCharacter;

        foreach (var skillSlot in _caster.Sheet.Skills)
        {
            if (!_allSkills.ContainsKey(skillSlot.slot))
            {
                _allSkills.Add(skillSlot.slot, new List<SkillSlot>());
            }
           
            _allSkills[skillSlot.slot].Add(skillSlot);
        }

        foreach (var skillPair in _allSkills)
        {
            MakeBag(skillPair.Key);
        }
    }

    void MakeBag(int level)
    {
        float totalChance = 0;
        List<SkillData> skills = new();

        foreach (var skillSlot in _allSkills[level])
        {
            if(!_shuffleBags.ContainsKey(level)) _shuffleBags.Add(level, new List<SkillData>());
            for (int i = 0; i < skillSlot.bagAmount; i++)
            {
                skills.Add(skillSlot.skill);
                
            }
        }

        for (int i = skills.Count - 1; i >= 0; i--)
        {
            int index = Random.Range(0, skills.Count);
            _shuffleBags[level].Add(skills[index]);
            skills.RemoveAt(index);
        }
    }

    SkillData GetSkillFromBag(int level)
    {
        var skill = _shuffleBags[level][0];
        _shuffleBags[level].RemoveAt(0);
        if(_shuffleBags[level].Count == 0) MakeBag(level);
        OnSkillSlotChanged.Invoke(level);
        return skill;
    }

    public void CastSkill(int level)
    {
        if (level >= _allSkills.Count)
        {
            Debug.LogError("Character does not have skills on slot " + level);
            return;
        }

        CastSkill(GetSkillFromBag(level));
    }

    public void CastSkill(SkillData skill)
    {
        if(skill == null) return;
        
        OnUseSkill.Invoke(skill);
        
        var effects = skill.Effects;
        var eCount = effects.Count;
        for (int i = 0; i < eCount; i++)
        {
            var targets = TargetManager.GetTargets(_caster, skill, effects[i]);
            SkillEffect.Activate(_caster, targets, effects[i]);
        }
    }
}
