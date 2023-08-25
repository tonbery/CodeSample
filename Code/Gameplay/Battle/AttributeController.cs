using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeController
{
    private Dictionary<EAttributes, AttributeInstance> _attributes;

    public void SetCharacter(CharacterSheet characterSheet) 
    {
        _attributes = new Dictionary<EAttributes, AttributeInstance>();
        
        for (int i = 0; i < (int)EAttributes.MAX; i++)
        {
            _attributes.Add((EAttributes)i, new AttributeInstance());
        }
        
        _attributes[EAttributes.MaxHealth].SetBaseValue(characterSheet.Attributes.maxHealth);
        _attributes[EAttributes.CurrentHealth].SetBaseValue(characterSheet.Attributes.maxHealth);
        _attributes[EAttributes.Power].SetBaseValue(characterSheet.Attributes.power);
        _attributes[EAttributes.ChargeSpeed].SetBaseValue(characterSheet.Attributes.chargeSpeed);
        _attributes[EAttributes.PassiveChargeSpeed].SetBaseValue(characterSheet.Attributes.passiveChargeSpeed);
        _attributes[EAttributes.RandomMin].SetBaseValue(characterSheet.Attributes.randomMin);
        _attributes[EAttributes.RandomMax].SetBaseValue(characterSheet.Attributes.randomMax);
    }

    public float GetValue(EAttributes attribute)
    {
        return _attributes[attribute].GetValue();
    }
    
    public AttributeInstance GetAttribute(EAttributes attribute)
    {
        return _attributes[attribute];
    }
}
