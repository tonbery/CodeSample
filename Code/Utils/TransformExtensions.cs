using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void Reset(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }
    
    public static void AddChild(this Transform trans, GameObject child, bool worldPositionStays = true)
    {
        child.transform.SetParent(trans, worldPositionStays);
    }
    public static void AddChild(this Transform trans, MonoBehaviour child, bool worldPositionStays = true)
    {
        child.transform.SetParent(trans, worldPositionStays);
    }
    
    public static void AddChild(this GameObject gameObject, GameObject child, bool worldPositionStays = true)
    {
        child.transform.SetParent(gameObject.transform, worldPositionStays);
    }
    public static void AddChild(this GameObject gameObject, MonoBehaviour child, bool worldPositionStays = true)
    {
        child.transform.SetParent(gameObject.transform, worldPositionStays);
    }
   
    public static void AddChild(this MonoBehaviour mono, MonoBehaviour child, bool worldPositionStays = true)
    {
        child.transform.SetParent(mono.transform, worldPositionStays);
    }
    public static void AddChild(this MonoBehaviour mono, GameObject child, bool worldPositionStays = true)
    {
        child.transform.SetParent(mono.transform, worldPositionStays);
    }
    
    
    
    public static RectTransform RectTransform(this Transform trans)
    {
        return (RectTransform)trans;
    }
    public static RectTransform RectTransform(this MonoBehaviour mono)
    {
        return (RectTransform)mono.transform;
    }
    public static RectTransform RectTransform(this GameObject gameObject)
    {
        return (RectTransform)gameObject.transform;
    }
    
    public static Transform Parent(this MonoBehaviour mono)
    {
        return mono.transform.parent;
    }
    public static Transform Parent(this GameObject gameObject)
    {
        return gameObject.transform.parent;
    }
    
}