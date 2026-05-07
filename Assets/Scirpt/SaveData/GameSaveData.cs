using System.Collections.Generic;

[System.Serializable]
public class GameSaveData
{
    
    public int totalCoins;
    public int currentXP;
    public int playerLevel;
    public int enemyUpgradePoints;
    public float enemyHpMul;
    public float enemyDmgMul;
    public int PlayerHealth;
    public int PlayerDamage;

    public List<string> unlockedSkillNames = new List<string>();
}