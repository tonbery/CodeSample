using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SkillEffectData
{
    public virtual ESkillEffect SkillEffectType() => ESkillEffect.Damage;
}

public class TargetedEffectData : SkillEffectData
{
    public enum ETargetTeam
    {
        Self,
        Friend,
        Enemy
    }
    public enum ETargetGroup
    {
        All,
        Count
    }

    public enum ETargetPriority
    {
        First,
        Last,
        LeastHealth,
        MostHealth,
        Random,
    }
    
    [SerializeField] public ETargetTeam targetTeam;
    [SerializeField] public ETargetGroup targetGroup;
    [SerializeField] public int count = 1;
    [SerializeField] public List<ETargetPriority> priority;
    [SerializeField] public float skillPower = 1;
}



[System.Serializable]
public class SE_Damage_Data:TargetedEffectData
{
    public override ESkillEffect SkillEffectType() => ESkillEffect.Damage;

}

[System.Serializable]
public class SE_Cure_Data:TargetedEffectData
{
    public override ESkillEffect SkillEffectType() => ESkillEffect.Cure;
}

[System.Serializable]
public class SE_PlaySound_Data:SkillEffectData
{
    public enum ESoundLocation
    {
        Self,
        LastTargets,
        Everywhere,
    }
    
    [SerializeField] private EventReference sound;
    [SerializeField] private ESoundLocation soundPosition;
    

    public EventReference Sound => sound;
    public ESoundLocation SoundPosition => soundPosition;

    public override ESkillEffect SkillEffectType() => ESkillEffect.PlaySound;
}



