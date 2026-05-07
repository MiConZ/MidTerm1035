using UnityEngine;

public class EnemyTreeManager : MonoBehaviour
{
    public static EnemyTreeManager Instance;

    [Header("Current Enemy Buffs")]
    public float totalHpMultiplier = 1f;
    public float totalDmgMultiplier = 1f;

    void Awake() => Instance = this;

    public void UpgradeEnemy(EnemySkillNodeSO node)
    {
        if (PlayerProfile.Instance.enemyUpgradePoints > 0 && !node.isUnlocked)
        {
           
            foreach (var pre in node.prerequisites)
            {
                if (!pre.isUnlocked) return;
            }

            node.isUnlocked = true;
            PlayerProfile.Instance.enemyUpgradePoints--;

            
            if (node.type == EnemySkillNodeSO.UpgradeType.HealthBoost)
                totalHpMultiplier *= node.bonusValue;
            else if (node.type == EnemySkillNodeSO.UpgradeType.DamageBoost)
                totalDmgMultiplier *= node.bonusValue;
           
            GameDataManager.Instance.SaveGame(); 
        }
    }
}