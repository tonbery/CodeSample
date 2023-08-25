using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    [SerializeField] private bool teleportOnContact = true; 
    [SerializeField, HideInInspector] private string sceneToGo;
    [SerializeField, HideInInspector] private string pointToGo;
    [SerializeField, HideInInspector] private string destination;

    [SerializeField] private PlayerSpawnPoint spawnPoint;

    public PlayerSpawnPoint SpawnPoint => spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!teleportOnContact) return;
        Teleport();
    }
    
    public void Teleport(){
        if (sceneToGo == String.Empty) return;
        GameInstance.GoToScene(sceneToGo, pointToGo);
    }
}
