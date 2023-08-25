using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    public UnityEvent<Usable> OnInteractionChange;

    private Usable _currentInteraction;
    private List<Usable> _objectsInRange = new List<Usable>();

    public Usable CurrentInteraction => _currentInteraction;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject);
        _objectsInRange.Add(other.GetComponent<Usable>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var interactiveObj = other.GetComponent<Usable>(); 
        _objectsInRange.Remove(interactiveObj);
    }

    void SetCurrentInteraction(Usable newInteractiveObject)
    {
        _currentInteraction = newInteractiveObject;
        OnInteractionChange.Invoke(_currentInteraction);
    }

    private void Update()
    {
        float closestDistance = 9999;
        Usable closestObj = null;
        
        foreach (var obj in _objectsInRange)
        {
            //if(!obj.CanReceiveInteraction) continue;
            
            var dist = Vector3.Distance(obj.transform.position, transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestObj = obj;
            }
        }

        if (closestObj != _currentInteraction)
        {
            SetCurrentInteraction(closestObj);
        }
    }
}
