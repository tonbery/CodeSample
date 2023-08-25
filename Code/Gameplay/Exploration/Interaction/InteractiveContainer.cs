using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveContainer : InteractiveObject
{
    [SerializeField] private bool destroyAfterInteraction;
    [SerializeField] private RewardData reward;
    [SerializeField] private Animator animator;
    private static readonly int Open = Animator.StringToHash("Open");

    public override void Interact()
    {
        GameInstance.GetCurrentSave().PickItem(reward);
        if(animator) animator.SetTrigger(Open);
        _canReceiveInteraction = false;
        if(destroyAfterInteraction) Destroy(gameObject);
    }
}
