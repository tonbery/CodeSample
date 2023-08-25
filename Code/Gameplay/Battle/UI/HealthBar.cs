using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private bool adjustPositionToCharacter = true;
    [SerializeField] private Vector2 offset;
    private BattleCharacter _battleCharacter;
    
    public void SetCharacter(BattleCharacter newCharacter)
    {
        _battleCharacter = newCharacter;
        _battleCharacter.OnDeath.AddListener(character => Destroy(gameObject));
        SetMaxValue(_battleCharacter.Attributes.GetValue(EAttributes.MaxHealth));
    }

    private void Update()
    {
        if (!_battleCharacter) return;
        
        SetMinValue(_battleCharacter.Attributes.GetValue(EAttributes.CurrentHealth));

        if (!adjustPositionToCharacter) return;
        
        GameObject WorldObject = _battleCharacter.gameObject;
        RectTransform UI_Element = (RectTransform)transform;
        RectTransform CanvasRect = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(WorldObject.transform.position);
        Vector2 WorldObject_ScreenPosition=new Vector2(
            ((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x*0.5f)),
            ((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y*0.5f)));
        UI_Element.anchoredPosition=WorldObject_ScreenPosition + offset;
    }
}
