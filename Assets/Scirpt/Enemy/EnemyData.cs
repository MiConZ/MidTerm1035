using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Base Stats")]
    public string enemyName;
    public float maxHealth;
    public float moveSpeed;
    public float attackDamage;

    [Header("Visuals")]
    public GameObject enemyPrefab; // ตัว Model ของศัตรู
    
    [Header("Rewards")]
    public int coinDrop;
    public int expValue;
}