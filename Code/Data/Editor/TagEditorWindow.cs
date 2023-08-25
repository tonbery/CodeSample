using UnityEditor;
using UnityEngine;

public class TagEditorWindow : EditorWindow
{
    private GameTags _gameTags;
    
    [MenuItem("Window/BRPG/Tags")]
    public static void ShowExample()
    {
        TagEditorWindow window = EditorWindow.CreateInstance<TagEditorWindow>();
        window.Show();

        window._gameTags = GameTags.GetAssetOnEditor();
    }
    
    Rect buttonRect;
    void OnGUI()
    {
        foreach (var tag in _gameTags.Tags)
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Label(tag);
            
            if (GUILayout.Button("Delete"))
            {
                _gameTags.RemoveTag(tag);
                GUILayout.EndHorizontal();
                break;
            }
            
            GUILayout.EndHorizontal();
        }
        
        
        
        //GUILayout.Label("Tag window with Popup example", EditorStyles.boldLabel);
        if (GUILayout.Button("New tag", GUILayout.Width(200)))
        {
            PopupWindow.Show(buttonRect, new NewTagPopup());
        }
        
        if (Event.current.type == EventType.Repaint) buttonRect = GUILayoutUtility.GetLastRect();
        
    }
}
