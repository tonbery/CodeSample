using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "CharacterSheet", menuName = "BRPG/Save/SaveGameData")]
public class SaveGameData : ScriptableObject
{
    [SerializeField] private MainSave save;

    public MainSave Save => save;
}
