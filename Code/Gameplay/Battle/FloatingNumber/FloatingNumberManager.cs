using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingNumberManager : MonoBehaviour
{
    [SerializeField] private Transform runtimeUI;
    
    public static FloatingNumberManager Instance;
    [SerializeField] private FloatingNumber floatingNumberPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowFloatingNumber(Vector3 position, float number, Color color)
    {
        var newNumber = Instantiate(floatingNumberPrefab);
        runtimeUI.AddChild(newNumber);
        newNumber.transform.localScale = Vector3.one;
        newNumber.SetData(position, number, color);
        
    }
}
