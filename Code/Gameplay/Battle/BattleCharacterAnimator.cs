using System.Collections;
using UnityEngine;



public class BattleCharacterAnimator : MonoBehaviour
{
    [SerializeField] private BattleCharacter battleCharacter;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    private static readonly int Charging = Animator.StringToHash("Charging");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Blink = Shader.PropertyToID("_Blink");

    private Coroutine _blinkCoroutine;

    private float _blinkTime = 0.1f;

    private void Start()
    {
        battleCharacter.SkillManager.OnUseSkill.AddListener(OnUseSkill);
        battleCharacter.OnDeath.AddListener(Death);
        battleCharacter.Attributes.GetAttribute(EAttributes.CurrentHealth).OnValueModify.AddListener(OnHealthModified);
    }

    private void Death(BattleCharacter arg0)
    {
        animator.Play("Death", 0, 0);
    }

    private void OnHealthModified(float modifier)
    {
        if (modifier < 0)
        {
            animator.SetBool(Hit, true);
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.material.SetInt(Blink, 1);
            }
           
            
            if(_blinkCoroutine != null) StopCoroutine(_blinkCoroutine);
            _blinkCoroutine = StartCoroutine(ResetBlink());
        }
    }

    private void OnUseSkill(SkillData skill)
    {
        animator.Play(skill.SkillAnimation.ToString(), 0, 0);
    }

    private void Update()
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingOrder = -(int)(transform.position.y * 10);
        }

        
        animator.SetBool(Charging, battleCharacter.IsCharging);
    }

    IEnumerator ResetBlink()
    {
        yield return null;
        
        animator.SetBool(Hit, false);
        
        yield return new WaitForSeconds(_blinkTime);

        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.material.SetInt(Blink, 0);
        }
       
        _blinkCoroutine = null;
    }


    public void SetCharacter(CharacterSheet characterSheet)
    {
        animator.runtimeAnimatorController = characterSheet.Animator;
    }
}
