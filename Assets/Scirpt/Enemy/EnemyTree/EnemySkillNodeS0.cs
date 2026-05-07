using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyNode", menuName = "ScriptableObjects/EnemySkillNode")]
public class EnemySkillNodeSO : ScriptableObject
{
    public string upgradeName;
    public bool isUnlocked;

    public enum UpgradeType { HealthBoost, DamageBoost, SpeedBoost }
    public UpgradeType type;
    public float bonusValue;

    public List<EnemySkillNodeSO> prerequisites;
}