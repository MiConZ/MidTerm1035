using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance;

    [Header("Player Stats")]
    public int totalCoins;
    public int currentXP;
    public int playerLevel = 1;

    [Header("Scaling Settings")]
    public int baseXPRequired = 100; 
    public float xpMultiplier = 1.2f; 
    public int bonusDamage = 0; 
    public int bonusHealth = 0;
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip levelup;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }


    public int GetRequiredXP()
    {
        return Mathf.RoundToInt(baseXPRequired * Mathf.Pow(xpMultiplier, playerLevel - 1));
    }

    public void AddRewards(int coins, int xp)
    {
        totalCoins += coins;
        currentXP += xp;

        while (currentXP >= GetRequiredXP())
        {
            LevelUp();
        }

        Debug.Log($"ได้รับ {coins} Coins และ {xp} XP!");
    }
    public int enemyUpgradePoints;
    void LevelUp()
    {
        currentXP -= GetRequiredXP();
        if (audioSource != null && levelup != null)
        {
            audioSource.PlayOneShot(levelup);
        }
        playerLevel++;
        Debug.Log($"Level Up! ตอนนี้เลเวล: {playerLevel}");

        if (playerLevel % 5 == 0)
        {
            bonusDamage += 2; 
            bonusHealth += 10;
            enemyUpgradePoints++; 
            Debug.Log("คุณได้รับ Enemy Upgrade Point! ไปอัปความโหดให้ศัตรูได้เลย");

            CoreBase.Instance.UpdateMaxHealth(bonusHealth);
            Debug.Log("โบนัสพิเศษ: เพิ่มดาเมจและเลือดสูงสุด!");
        }
    }

  

}
