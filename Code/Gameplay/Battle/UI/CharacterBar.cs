using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBar : MonoBehaviour
{
    [Header("Library")]
    [SerializeField] private BattleSkillIcon skillIconPrefab;
    [SerializeField] private GameObject barPartitionPrefab;
    
    [Header("Scene")]
    [SerializeField] private Bar bar;
    [SerializeField] private Transform skillList;
    [SerializeField] private HealthBar healthBar;

    [Header("Config")] 
    [SerializeField] private bool usePartition;
    

    private List<BattleSkillIcon> _skillsSlots = new();

    private BattleCharacter _battleCharacter;

   

    public void SetCharacter(BattleCharacter character)
    {

        _battleCharacter = character;
        bar.SetMaxValue(1);

        var segments = _battleCharacter.SkillManager.ShuffleBags.Count;
        _battleCharacter.SkillManager.OnSkillSlotChanged.AddListener(OnSkillSlotChanged);
        
        var segmentSize = bar.Parent().RectTransform().sizeDelta.x / segments;

        for (int i = 0; i < segments; i++)
        {
            var newSkill = Instantiate(skillIconPrefab);
            _skillsSlots.Add(newSkill);
            skillList.transform.AddChild(newSkill);
            newSkill.SetSkill(_battleCharacter.SkillManager.ShuffleBags[i][0]);
            newSkill.transform.localScale = Vector3.one;
            newSkill.RectTransform().anchorMin = new Vector2(0, 1f);
            newSkill.RectTransform().anchorMax = new Vector2(0, 1f);
            newSkill.RectTransform().pivot = new Vector2(0.5f, 1f);
            newSkill.RectTransform().anchoredPosition = new Vector2(segmentSize * i + (segmentSize/2), 0);

            if(i == 0) continue;

            if (usePartition)
            {
                var newBarPartition = Instantiate(barPartitionPrefab);
                bar.AddChild(newBarPartition);
                newBarPartition.transform.localScale = Vector3.one;
                newBarPartition.RectTransform().anchoredPosition = new Vector2(segmentSize * i, 0);
            }
        }
        
        healthBar.SetCharacter(character);
    }

    private void OnSkillSlotChanged(int slot)
    {
        _skillsSlots[slot].SetSkill(_battleCharacter.SkillManager.ShuffleBags[slot][0]);
    }

    private void Update()
    {
        if (!_battleCharacter) return;
        bar.SetMinValue(_battleCharacter.ChargeValue);
        
        for (int i = 0; i < _skillsSlots.Count; i++)
        {
            if (i == _battleCharacter.CurrentSlot)
            {
                _skillsSlots[i].transform.localScale = Vector3.one;
            }
            else
            {
                _skillsSlots[i].transform.localScale = Vector3.one * 0.5f;
            }
        }
        
    }
   
}
