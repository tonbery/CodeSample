using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameTags : ScriptableObject
{
    [SerializeField] private List<string> tags;
    [SerializeField] private List<string> teleportTags;

    public List<string> Tags => tags;

    public List<string> TeleportTags => teleportTags;

    public static GameTags GetInstance()
    {
        var gameTags = AssetDatabase.FindAssets("t:GameTags");
        return AssetDatabase.LoadAssetAtPath<GameTags>(AssetDatabase.GUIDToAssetPath(gameTags[0]));
    }

    public int GetTeleportIndex(string teleport)
    {
        return ArrayUtility.IndexOf(teleportTags.ToArray(), teleport);
    }

    public void SetTeleportTags(string sceneName, List<string> teleports)
    {
        for (int i = teleportTags.Count - 1; i >= 0; i--)
        {
            if (teleportTags[i].Contains(sceneName)) teleportTags.RemoveAt(i); 
        }

        foreach (var teleport in teleports)
        {
            teleportTags.Add(sceneName+"/"+teleport);
        }

        teleportTags.Sort();
        
        EditorUtility.SetDirty(this);
    }

    public bool HaveTag(string tag)
    {
        return tags.Contains(tag);
    }

    public void AddTag(string tag)
    {
        tags.Add(tag);
        tags.Sort();
        EditorUtility.SetDirty(this);
    }

    public void RemoveTag(string tag)
    {
        tags.Remove(tag);
        tags.Sort();
        EditorUtility.SetDirty(this);
    }

    public static GameTags GetAssetOnEditor()
    {
        GameTags tags;
        var gameTags = AssetDatabase.FindAssets("t:GameTags");
        
        if (gameTags.Length == 0)
        {
            tags = CreateInstance<GameTags>();
            AssetDatabase.CreateAsset(tags, "Assets/BRPG/Data/Tags/GameTags.asset");
        }
        else
        {
            tags = AssetDatabase.LoadAssetAtPath<GameTags>(AssetDatabase.GUIDToAssetPath(gameTags[0]));
        }

        return tags;
    }
}
