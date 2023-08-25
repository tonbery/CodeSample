using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSave
{
    [SerializeField] private CharacterSheet character;
    [SerializeField] private int experience;
    [SerializeField] private int level;
}
