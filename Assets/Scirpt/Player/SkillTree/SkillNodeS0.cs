
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "ScriptableObjects/SkillNode")]
public class SkillNodeSO : ScriptableObject
{
    public string skillName;
    public bool isUnlocked;
    public int cost;


    public enum SkillType { ActiveSkill, BonusDamage, BonusHealth }
    public SkillType type;
    public int statValue; 

    public List<SkillNodeSO> prerequisites;
   
    public bool IsAvailable()
    {
        if (isUnlocked) return false;
        if (prerequisites.Count == 0) return true;

        foreach (var pre in prerequisites)
        {
            if (!pre.isUnlocked) return false;
        }
        return true;
    }

}