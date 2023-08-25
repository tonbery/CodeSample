using System;
using System.Collections.Generic;


public static class TargetManager
{
    public static List<BattleCharacter> GetTargets(BattleCharacter character, SkillData skill, SkillEffectData effect)
    {
        if (effect is not TargetedEffectData data) return new List<BattleCharacter>();
        
        if (data.targetTeam == TargetedEffectData.ETargetTeam.Self) return new List<BattleCharacter> { character };
        
        List<BattleCharacter> possibleTargets = new List<BattleCharacter>();
        
        if (data.targetTeam == TargetedEffectData.ETargetTeam.Enemy) possibleTargets.AddRange(BattleManager.Instance.GetOppositeTeam(character));
        else if (data.targetTeam == TargetedEffectData.ETargetTeam.Friend) possibleTargets.AddRange(BattleManager.Instance.GetTeam(character));

        for (int i = possibleTargets.Count - 1; i >= 0; i--)
        {
            if(!possibleTargets[i].Alive) possibleTargets.RemoveAt(i);
        }
        
        if (data.targetGroup == TargetedEffectData.ETargetGroup.All) return possibleTargets;
        
        List<BattleCharacter> targets = new ();

        for (int i = 0; i < data.count; i++)
        {
            if(possibleTargets.Count == 0) break;
            
            if (data.priority.Count == 0)
            {
                targets.Add(possibleTargets[0]);
                possibleTargets.RemoveAt(0);    
            }
            else
            {
                var best = GetBestTarget(possibleTargets, data.priority);
                possibleTargets.Remove(best);
                targets.Add(best);
            }
        }

        return targets;
    }

    static BattleCharacter GetBestTarget(List<BattleCharacter> targets, List<TargetedEffectData.ETargetPriority> priorities)
    {
        BattleCharacter best = null;
        
        foreach (var priority in priorities)
        {
            switch (priority)
            {
                case TargetedEffectData.ETargetPriority.Random:
                    return targets[UnityEngine.Random.Range(0, targets.Count-1)];
                
                case TargetedEffectData.ETargetPriority.First:
                    return targets[0];
                    
                case TargetedEffectData.ETargetPriority.Last:
                    return targets[targets.Count-1];
                    
                case TargetedEffectData.ETargetPriority.LeastHealth:
                    float leastHealth = 999999999;
                    
                    foreach (var target in targets)
                    {
                        if (target.Attributes.GetValue(EAttributes.CurrentHealth) < leastHealth)
                        {
                            leastHealth = target.Attributes.GetValue(EAttributes.CurrentHealth);
                            best = target;
                        }
                    }

                    return best;
                    
                    break;
                case TargetedEffectData.ETargetPriority.MostHealth:
                    float mostHealth = 0;
                    
                    foreach (var target in targets)
                    {
                        if (target.Attributes.GetValue(EAttributes.CurrentHealth) > mostHealth)
                        {
                            mostHealth = target.Attributes.GetValue(EAttributes.CurrentHealth);
                            best = target;
                        }
                    }

                    return best;
            }
        }

        return null;
    }
}
