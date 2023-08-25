using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EncounterData))]
[CanEditMultipleObjects]
public class EncounterDataEditor : Editor
{
    void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        /*if (!Application.isPlaying)
        {
            if (GUILayout.Button("Play and Spawn Encounter"))
            {
                EditorApplication.playModeStateChanged += OnPlayModeChanged;
                EditorApplication.EnterPlaymode();
                
            }
           
        }
        else
        {*/
        /*if (Application.isPlaying)
        {
            if (GUILayout.Button(Application.isPlaying?"!!!Spawn Encounter!!!":"Play and Spawn Encounter"))
            {
                LoadEncounter();
            }   
        }*/ 
            
       // }
        
        
        

        DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();
    }

    /*private void OnPlayModeChanged(PlayModeStateChange obj)
    {
        Debug.Log(obj);
        if (obj == PlayModeStateChange.EnteredPlayMode)
        {
            var nGo = new GameObject("DELETE");
            nGo.AddComponent<CoroutineHolder>().StartCoroutine(WaitTest());
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;
        }
    }

    IEnumerator WaitTest()
    {
        yield return new WaitForSeconds(1);
        LoadEncounter();
    }

    void LoadEncounter()
    {
        var EM = ExplorationManager.Instance;
        Vector3 characterLocation = Vector3.zero;
            
        if (EM != null)
        {
            characterLocation = EM.PlayerCharacter.transform.position;
        }
        GameInstance.LoadBattle(((EncounterData)serializedObject.targetObject).MakeRuntimeEncounter(), characterLocation);
    }*/
}
