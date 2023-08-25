using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BattleCharacter : MonoBehaviour
{
    private AttributeController _attributes;
    private CharacterSheet _characterSheet;
    private SkillManager _skillManager;

    private GameObject _currentPositionTarget;
    private int _characterIndex;
    private int _currentPointIndex;
    private int _maxSkillLevel;
    private bool _isAI;
    private int _team;
    private bool _alive = true;
    
    private bool _isSelected;
    private float _currentCharge;
    private float ChargePercentage => _currentCharge / _maxCharge;
    public float ChargeValue => Sheet.ChargeCurve.Evaluate(ChargePercentage);
    private float _maxCharge = 100;
    private float _maxUnselectedChargePercentage = 0.49f;
    private bool _isCharging;
    private int _currentSlot;

    public UnityEvent<bool> OnSelectChange = new();
    public UnityEvent<BattleCharacter> OnChangePoint = new();
    public UnityEvent<BattleCharacter> OnDeath = new();
    public float CurrentCharge => _currentCharge;
    public float MaxCharge => _maxCharge;

    public GameObject CurrentPositionTarget => _currentPositionTarget;

    public int CurrentPointIndex => _currentPointIndex;

    public int CharacterIndex => _characterIndex;

    public bool IsSelected => _isSelected;

    public AttributeController Attributes => _attributes;

    public int MaxSkillLevel => _maxSkillLevel;

    public CharacterSheet Sheet => _characterSheet;

    public int CurrentSlot => _currentSlot;

    public SkillManager SkillManager => _skillManager;

    public bool IsCharging => _isCharging && _isSelected;

    public int Team => _team;

    public bool Alive => _alive;

    private int _squadIndex = -1;

    public void SetCharacter(CharacterSheet characterSheet, int index, bool isAI, int team)
    {
        _characterIndex = index;
        _characterSheet = characterSheet;
        _attributes = new AttributeController();
        _attributes.SetCharacter(_characterSheet);
        _attributes.GetAttribute(EAttributes.CurrentHealth).OnValueModify.AddListener(OnHealthModified);
        _attributes.GetAttribute(EAttributes.CurrentHealth).OnValueChange.AddListener(OnUpdateCurrentHealth);
        
        _team = team;
        _isAI = isAI;

        if (_isAI)
        {
            var ai = gameObject.AddComponent<BattleEnemyAI>();
            ai.SetCharacter(this);
        }

        if (_team == 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        int maxFoundLevel = 0;
        foreach (var skill in _characterSheet.Skills)
        {
            if (skill.slot > maxFoundLevel)
            {
                maxFoundLevel = skill.slot;
            }
        }

        _maxSkillLevel = maxFoundLevel;

        _skillManager = new SkillManager();
        _skillManager.SetCharacter(this);

        GetComponent<BattleCharacterAnimator>().SetCharacter(_characterSheet);
    }

    private void OnHealthModified(float modifier)
    {
        if(modifier < 0) FloatingNumberManager.Instance.ShowFloatingNumber(transform.position, modifier, Color.red);
        else if (modifier > 0) FloatingNumberManager.Instance.ShowFloatingNumber(transform.position, modifier, Color.green);
    }

    private void OnUpdateCurrentHealth(float currentHealth)
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        _alive = false;
        OnDeath.Invoke(this);
        if(_isAI) Destroy(gameObject);
    }


    public void StartCharge()
    {
        _isCharging = true;
        SelectCharacter();
    }

    public void StopCharge(bool cast = true)
    {
        _isCharging = false;
        if (!cast) return;
        
        _skillManager.CastSkill(CurrentSlot);
        _currentCharge = 0;
    }

    public void SelectCharacter()
    {
        if (!_isSelected)
        {
            _isSelected = true;
            OnSelectChange.Invoke(_isSelected);
        }
    }

    public void SetPositionTarget(GameObject newTarget, int newIndex)
    {
        _currentPositionTarget = newTarget;
        _currentPointIndex = newIndex;

        OnChangePoint.Invoke(this);
    }

    public void UnselectCharacter()
    {
        if (_isSelected)
        {
            _isSelected = false;
            StopCharge(false);
            OnSelectChange.Invoke(_isSelected);
        }
    }

    private void Update()
    {
        if (_currentPositionTarget)
        {
            transform.position = Vector3.Lerp(transform.position, _currentPositionTarget.transform.position, 5 * Time.deltaTime);
        }

        if (!_alive) return;
        
        if (!_isSelected && ChargeValue < _maxUnselectedChargePercentage)
        {
            AddCharge(_attributes.GetValue(EAttributes.PassiveChargeSpeed) * BattleTime.DeltaTime);
        }

        if (_isCharging)
        {
            AddCharge(_attributes.GetValue(EAttributes.ChargeSpeed) * BattleTime.DeltaTime);
        }

        _currentSlot = Mathf.Clamp((int)Mathf.Lerp(0, _maxSkillLevel+1, ChargeValue), 0, _maxSkillLevel);
    }

    void AddCharge(float charge)
    {
        
        _currentCharge = Mathf.Clamp(_currentCharge + charge, 0, _maxCharge);
    }

    public void TakeDamage(float damage)
    {
        if (!_alive) return;
        float realAmount = (int)Mathf.Max(1, damage);
        _attributes.GetAttribute(EAttributes.CurrentHealth).ModifyCurrentValue(-realAmount);
    }
    
    public void Cure(float amount)
    {
        float realAmount = (int)Mathf.Max(1, amount);
        _attributes.GetAttribute(EAttributes.CurrentHealth).ModifyCurrentValue(realAmount);
    }

    private void OnGUI()
    {
        if (_team != 0) return;
        GUILayout.Label(ChargeValue.ToString());
        GUILayout.Label(_currentSlot.ToString());
        
    }
}
