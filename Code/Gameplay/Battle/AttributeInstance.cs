using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttributeInstance
{
    private float _baseValue;
    private float _currentValue;
    public UnityEvent<float> OnValueModify = new();
    public UnityEvent<float> OnValueChange = new();
    
    public void SetBaseValue(float newValue)
    {
        _baseValue = newValue;
        _currentValue = newValue;
        OnValueChange.Invoke(_currentValue);
    }

    public void ModifyCurrentValue(float value)
    {
        _currentValue = Mathf.Clamp(_currentValue+ value, 0, _baseValue);
        OnValueChange.Invoke(_currentValue);
        OnValueModify.Invoke(value);
    }
    
    public float GetValue()
    {
        return _currentValue;
    }

}
