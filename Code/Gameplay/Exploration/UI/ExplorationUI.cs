using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class ExplorationUI : MonoBehaviour
{
    [SerializeField] private WorldToScreenSpaceUI interactionPrompt;
    
    
    IEnumerator Start()
    {
        Debug.Log("start");
        interactionPrompt.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(0.1f);
        ExplorationManager.Instance.PlayerCharacter.Interaction.OnInteractionChange.AddListener(Call);
    }

    private void Call(Usable interactiveObject)
    {
        if (interactiveObject != null)
        {
            interactionPrompt.SetTarget(interactiveObject.gameObject);
            interactionPrompt.gameObject.SetActive(true);
        }
        else
        {
            interactionPrompt.gameObject.SetActive(false);    
        }
        
        
    }
}
