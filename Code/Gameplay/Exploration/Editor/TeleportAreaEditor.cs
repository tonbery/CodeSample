using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TeleportArea))]
[CanEditMultipleObjects]
public class TeleportAreaEditor : Editor
{
    private SerializedProperty _destination;
    private SerializedProperty _sceneToGo;
    private SerializedProperty _pointToGo;

    private int _selectionIndex = -1;
    
    void OnEnable()
    {
        _destination = serializedObject.FindProperty("destination");
        _sceneToGo = serializedObject.FindProperty("sceneToGo");
        _pointToGo = serializedObject.FindProperty("pointToGo");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();
        
        if (_destination.stringValue != String.Empty)
        {
            _selectionIndex = GameTags.GetInstance().GetTeleportIndex(_destination.stringValue);
        }

        GUILayout.Label("Destination");
        _selectionIndex = EditorGUILayout.Popup(_selectionIndex, GameTags.GetInstance().TeleportTags.ToArray());

        if (_selectionIndex > -1)
        {
            _destination.stringValue = GameTags.GetInstance().TeleportTags[_selectionIndex];
            _sceneToGo.stringValue = _destination.stringValue.Split('/')[0];
            _pointToGo.stringValue = _destination.stringValue.Split('/')[1];
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
