using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MinMax))]
public class MinMaxDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        var minVal = property.FindPropertyRelative("min");
        var maxVal  = property.FindPropertyRelative("max");

        GUILayout.BeginHorizontal();
        GUILayout.Label(label);
        EditorGUILayout.PropertyField(minVal, GUIContent.none);
        EditorGUILayout.PropertyField(maxVal, GUIContent.none);
        GUILayout.EndHorizontal();

        EditorGUI.EndProperty();
    }
}

