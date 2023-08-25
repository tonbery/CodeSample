using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FloatingNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;
    public void SetData(Vector3 position, float number, Color color, bool unsigned = true)
    {
        RectTransform UI_Element = (RectTransform)transform;
        RectTransform CanvasRect = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(position + Vector3.up);
        Vector2 WorldObject_ScreenPosition=new Vector2(
            ((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x*0.5f)),
            ((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y*0.5f)));
        UI_Element.anchoredPosition=WorldObject_ScreenPosition;

        numberText.text = (unsigned?Mathf.Abs(number):number).ToString();

        numberText.color = color;

        UI_Element.DOAnchorPos(UI_Element.anchoredPosition + (Vector2.up * 100), 0.3f).onComplete
            += () => numberText.DOFade(0, 0.5f).onComplete
                += () => { Destroy(gameObject); };
    }
}
