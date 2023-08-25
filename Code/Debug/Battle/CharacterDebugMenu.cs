using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDebugMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Button killButton;
    [SerializeField] private Button cure1Button;
    [SerializeField] private Button cure10Button;
    [SerializeField] private Button damage1Button;
    [SerializeField] private Button damage10Button;
    
    private BattleCharacter _character;

    private void Start()
    {
        killButton.onClick.AddListener(() => _character.TakeDamage(1000));
        cure1Button.onClick.AddListener(() => _character.Cure(1));
        cure10Button.onClick.AddListener(() => _character.Cure(10));
        damage1Button.onClick.AddListener(() => _character.TakeDamage(1));
        damage10Button.onClick.AddListener(() => _character.TakeDamage(10));
    }

    public void SetCharacter(BattleCharacter character)
    {
        _character = character;

        characterName.text = character.Sheet.name;

        var position = character.transform.position;
        
        RectTransform UI_Element = (RectTransform)transform;
        RectTransform CanvasRect = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(position + Vector3.up);
        Vector2 WorldObject_ScreenPosition=new Vector2(
            ((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x*0.5f)),
            ((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y*0.5f)));
        UI_Element.anchoredPosition=WorldObject_ScreenPosition;
    }
}
