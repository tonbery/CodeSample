using UnityEditor;
using UnityEngine;

public class NewTagPopup : PopupWindowContent
{
    string tagName = "";
    
    private GameTags _gameTags;
    
    public override Vector2 GetWindowSize()
    {
        return new Vector2(200, 150);
    }

    public override void OnGUI(Rect rect)
    {
        GUILayout.Label("Popup Options Example", EditorStyles.boldLabel);
        tagName = EditorGUILayout.TextField("Tag", tagName);

        if (tagName == string.Empty)
        {
            EditorGUILayout.HelpBox("Tags cannot be empty", MessageType.Error);
            return;
        }
        
        if (_gameTags.HaveTag(tagName))
        {
            EditorGUILayout.HelpBox("Tag "+ tagName + " already exists", MessageType.Error);
            return;
        }
        
        if (GUILayout.Button("Create"))
        {
            _gameTags.AddTag(tagName);
            editorWindow.Close();
        }
    }

    public override void OnOpen()
    {
        _gameTags = GameTags.GetAssetOnEditor();
        Debug.Log("Popup opened: " + this);
    }

    public override void OnClose()
    {
        Debug.Log("Popup closed: " + this);
    }
}
