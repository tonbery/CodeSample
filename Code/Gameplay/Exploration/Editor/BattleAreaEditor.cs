using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(BattleArea))]
//[CanEditMultipleObjects]
public class BattleAreaEditor : Editor
{
    private SerializedProperty _battleAreaData;
    void OnEnable()
    {
        _battleAreaData = serializedObject.FindProperty("battleAreaData");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

    
        serializedObject.ApplyModifiedProperties();
    }
}
