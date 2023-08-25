using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DebugObjectTarget : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent<DebugObjectTarget> OnClick = new ();

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke(this);
    }
}
