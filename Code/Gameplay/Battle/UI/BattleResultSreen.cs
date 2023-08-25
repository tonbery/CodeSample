using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleResultSreen : MonoBehaviour
{
    [SerializeField] private Button okButton;
    [SerializeField] private GameObject graphics;

    protected virtual void Start()
    {
        okButton.onClick.AddListener(OkPressed);
    }

    protected void Open()
    {
        graphics.SetActive(true);
    }
    
    protected virtual void OkPressed()
    {
        
    }
}
