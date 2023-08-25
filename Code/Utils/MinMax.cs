using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinMax
{
    [SerializeField] private float min = 0;
    [SerializeField] private float max = 1;
    
    public float GetRandomValue()
    {
        return Random.Range(min, max);
    }
    
    public int GetRandomValueI()
    {
        return (int)Random.Range(min, max);
    }

}
