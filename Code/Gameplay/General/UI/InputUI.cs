using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionName;
    [SerializeField] private TextMeshProUGUI inputName;
    [SerializeField] private Image inputImage;
    
    [SerializeField] private bool showActionName = false;
    [SerializeField] private bool showInputName = false;
    [SerializeField] private bool showInputImage = true;

    private void OnValidate()
    {
        actionName.gameObject.SetActive(showActionName);
        inputName.gameObject.SetActive(showInputName);
        inputImage.gameObject.SetActive(showInputImage);
    }

    public void SetKeySprite(Sprite sprite)
    {
        inputImage.sprite = sprite;
    }

}
