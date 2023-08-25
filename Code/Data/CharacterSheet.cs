using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct AttributeSet
{
    public float maxHealth;
    public float power;
    public float chargeSpeed;
    public float passiveChargeSpeed;
    public float randomMin;
    public float randomMax;
}

[System.Serializable]
public struct SkillSlot
{
    [FormerlySerializedAs("level")] public int slot;
    public SkillData skill;
    public float bagAmount;
    [Header("AI")] 
    public float chargeTime;
}

public enum EAttributes
{
    MaxHealth,
    CurrentHealth,
    Power,
    ChargeSpeed,
    PassiveChargeSpeed,
    RandomMin,
    RandomMax,
    MAX
}

[System.Serializable]
[CreateAssetMenu(fileName = "CharacterSheet", menuName = "BRPG/Data/Character")]
public class CharacterSheet : ScriptableObject
{
    [SerializeField] private AnimatorOverrideController animator;
    [SerializeField] private AnimationCurve chargeCurve;
    [SerializeField] private AttributeSet attributeSet;
    [SerializeField] private List<SkillSlot> skills;
    [SerializeField] private List<ItemSlot> items;

    public AttributeSet Attributes => attributeSet;
    public List<SkillSlot> Skills => skills;
    public AnimationCurve ChargeCurve => chargeCurve;
    public AnimatorOverrideController Animator => animator;
}
