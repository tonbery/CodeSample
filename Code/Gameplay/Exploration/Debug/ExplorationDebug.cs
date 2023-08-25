using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationDebug : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DebugInventory();
        }
    }

    private void DebugInventory()
    {
        string debugString = "[INVENTORY]\n";
        foreach (var itemSlot in GameInstance.GetCurrentSave().items)
        {
            debugString += itemSlot.item.name + " [" + itemSlot.count + "]\n";
        }
        
        Debug.Log(debugString);

    }
}
