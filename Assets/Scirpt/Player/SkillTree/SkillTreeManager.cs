using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    bool success = false;
    public bool CanUnlock(SkillNodeSO skill)
    {
        if (skill.isUnlocked) return false; 
        if (skill.prerequisites.Count == 0) return true; 

       
        foreach (var pre in skill.prerequisites)
        {
            if (!pre.isUnlocked) return false; 
        }

        return true;
    }

    public void TryUnlockSkill(SkillNodeSO skill)
    {
        if (CanUnlock(skill))
        {
            
            if (PlayerProfile.Instance.totalCoins >= skill.cost)
            {
                PlayerProfile.Instance.totalCoins -= skill.cost;
                skill.isUnlocked = true;
                Debug.Log($"ปลดล็อก {skill.skillName} สำเร็จ!");
            }
        }
    }
    public bool IsPathClear(SkillNodeSO targetSkill)
    {

        if (targetSkill.isUnlocked) return true;

        if (targetSkill.prerequisites.Count == 0) return !targetSkill.isUnlocked;

        foreach (var pre in targetSkill.prerequisites)
        {
         
            if (!IsPathClear(pre)) return false;
        }

        return true;
    }

    public void OnSkillClick(SkillNodeSO clickedSkill)
    {
        if (clickedSkill.IsAvailable())
        {

            if (PlayerProfile.Instance.totalCoins >= clickedSkill.cost)
            {
                PlayerProfile.Instance.totalCoins -= clickedSkill.cost;
                clickedSkill.isUnlocked = true;
                success = true; 
                Debug.Log($"ปลดล็อก {clickedSkill.skillName} สำเร็จ!");
                GameDataManager.Instance.SaveGame();
            }
            else
            {
                Debug.Log("เงินไม่พอครับ!");
            }
        }
        if (success)
        {
           
            SkillButtonUI[] allButtons = FindObjectsOfType<SkillButtonUI>();
            foreach (var btn in allButtons)
            {
                btn.UpdateVisual();
            }
        }
    }
    

  
}