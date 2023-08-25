using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillIcon : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void SetSkill(SkillData skill)
    {
        icon.sprite = skill.SkillIcon;
    }
}
