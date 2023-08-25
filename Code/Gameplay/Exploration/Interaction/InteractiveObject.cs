#if UNITY_EDITOR
using UnityEditor;
#endif


using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private Collider2D interactionCollider;
    protected bool _canReceiveInteraction;

    public bool CanReceiveInteraction => _canReceiveInteraction;

    public virtual void Interact()
    {
        
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (interactionCollider == null)
        {
            interactionCollider = GetComponent<CircleCollider2D>();
            interactionCollider.isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("Interaction");
            EditorUtility.SetDirty(this);
        }
    }
#endif
    
}
