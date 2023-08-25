
using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Serialization;

public enum ESkillEffect
{
    Damage,
    Cure,
    PlaySound,
}

public enum EAnimation
{
    Skill_1,
    Skill_2,
    Skill_3,
}

[CreateAssetMenu(fileName = "Skill", menuName = "BRPG/Data/Skill")]
public class SkillData : ScriptableObject
{
    [SerializeField] private EAnimation skillAnimation;
    [SerializeField, ShowAssetPreview()] private Sprite skillIcon;
    [SerializeReference] private List<SkillEffectData> effects = new();
    
    public Sprite SkillIcon => skillIcon;
    public List<SkillEffectData> Effects => effects;
    public EAnimation SkillAnimation => skillAnimation;

    [SerializeField] private ESkillEffect effectToAdd;

    [Button("Add")]
    void AddEffect()
    {
        switch (effectToAdd)
        {
            case ESkillEffect.Damage:
                effects.Add(new SE_Damage_Data());
                break;
            case ESkillEffect.Cure:
                effects.Add(new SE_Cure_Data());
                break;
            case ESkillEffect.PlaySound:
                effects.Add(new SE_PlaySound_Data());
                break;
        }
    }
}