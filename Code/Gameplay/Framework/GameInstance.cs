using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameInstance : MonoBehaviour
{
    [SerializeField] private SaveGameData defaultNewGame;

    private static GameInstance _instance;

    public static string LastScene;
    public static string NextSpawnPoint;
    public static Vector3 LastPlayerPosition;
    public static RuntimeEncounter RuntimeEncounter;
    public static bool CameFromBattle;

    private static  string _currentSaveName;
    private static MainSave _currentSave;
    public static MainSave GetCurrentSave()
    {
        if (_currentSave == null)
        {
            NewGame("Editor");
        }

        return _currentSave;
    }

    public static GameInstance Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static void NewGame(string saveName)
    {
        _currentSaveName = saveName;
        _currentSave = _instance.defaultNewGame.Save;
        _currentSave.saveName = _currentSaveName;
        ES3.Save(_currentSaveName, _currentSave);
    }

    public static void Save()
    {
        ES3.Save(_currentSaveName, _currentSave);
    }
    
    public static void LoadGame(string saveName)
    {
        _currentSaveName = saveName;
        Debug.Log("load game " + saveName);
        _currentSave = ES3.Load<MainSave>(_currentSaveName);
        Debug.Log("value is " + _currentSave.saveName);
    }
    
    public static List<string> GetSaveSlots()
    {
        if (!ES3.FileExists()) return new List<string>();
        
        var files = ES3.GetKeys();
        List<string> fileNames = new List<string>();
        
        foreach (var file in files)
        {
            fileNames.Add(file);
        }

        return fileNames;
    }

    public static void LoadBattle(RuntimeEncounter encounterData, Vector3 playerPosition)
    {
        LastScene = SceneManager.GetActiveScene().name;
        RuntimeEncounter = encounterData;
        LastPlayerPosition = playerPosition;
        CameFromBattle = true;
        SceneManager.LoadScene("Battle");
    }

    public static void BackToExploration()
    {
        SceneManager.LoadScene(LastScene);
    }

    public static void GoToScene(string sceneName, string spawnPoint)
    {
        CameFromBattle = false;
        LastScene = sceneName;
        NextSpawnPoint = spawnPoint;
        SceneManager.LoadScene(sceneName);
    }
}

public static class BattleTime
{
    public static float TimeScale = 1;
    public static float DeltaTime => Time.deltaTime * TimeScale;

    public static bool IsOnPause { get; private set; }

    static void SetBattleTimeScale(float newTimeScale)
    {
        TimeScale = newTimeScale;
        IsOnPause = TimeScale == 0;
    }
    
    public static void Pause()
    {
        SetBattleTimeScale(0);
    }
    
    public static void UnPause()
    {
        SetBattleTimeScale(1);
    }

}
