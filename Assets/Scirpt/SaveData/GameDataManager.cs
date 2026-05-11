using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
   

    public List<SkillNodeSO> allSkills;
    public List<EnemySkillNodeSO> allEnemySkills;

    private string savePath;
    void Start()
    {

        LoadGame();
    }
    void Awake()
    {
        if (Instance == null) Instance = this;
        savePath = Path.Combine(Application.persistentDataPath, "player_save.json");
    }
   
    public void SaveGame()
    {
        int currentBonusDamage = 0;
        int currentBonusHealth = 0;
        

        foreach (var skill in allSkills)
        {
            
            if (skill != null && skill.isUnlocked)
            {
                if (skill.type == SkillNodeSO.SkillType.BonusDamage)
                    currentBonusDamage += skill.statValue;
                else if (skill.type == SkillNodeSO.SkillType.BonusHealth)
                    currentBonusHealth += skill.statValue;
            }
        }
        if (PlayerProfile.Instance != null)
        {
            PlayerProfile.Instance.bonusDamage = currentBonusDamage;
            PlayerProfile.Instance.bonusHealth = currentBonusHealth;
        }
        GameSaveData data = new GameSaveData();
        if (PlayerProfile.Instance != null)
        {
            
            data.totalCoins = PlayerProfile.Instance.totalCoins;
            data.currentXP = PlayerProfile.Instance.currentXP;
            data.playerLevel = PlayerProfile.Instance.playerLevel;
            data.enemyUpgradePoints = PlayerProfile.Instance.enemyUpgradePoints;
            data.enemyHpMul = EnemyTreeManager.Instance.totalHpMultiplier;
            data.enemyDmgMul = EnemyTreeManager.Instance.totalDmgMultiplier;
            data.PlayerDamage = PlayerProfile.Instance.bonusDamage;
            data.PlayerHealth = PlayerProfile.Instance.bonusHealth;
        }
        else
        {

            data.PlayerDamage = currentBonusDamage;
            data.PlayerHealth = currentBonusHealth;
        }

        data.unlockedSkillNames.Clear();
        foreach (var skill in allSkills)
        {
            if (skill.isUnlocked)
            {
                data.unlockedSkillNames.Add(skill.name);

            }
        }
       


        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("บันทึกข้อมูลทั้งหมดลงไฟล์เดียวเรียบร้อย!");
    }



    public bool IsSkillUnlocked(string targetSkillName)
    {
        
        foreach (var skill in allSkills)
        {
            if (skill != null && skill.name == targetSkillName)
            {
                return skill.isUnlocked;
            }
        }

        Debug.LogWarning("ค้นหาสกิลไม่พบ: " + targetSkillName);
        return false; 
    }
    public void LoadGame()
    {
        if (!File.Exists(savePath)) return;

        string json = File.ReadAllText(savePath);
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);

       
        PlayerProfile.Instance.totalCoins = data.totalCoins;
        PlayerProfile.Instance.currentXP = data.currentXP;
        PlayerProfile.Instance.playerLevel = data.playerLevel;
        if (PlayerProfile.Instance != null)
        {
            PlayerProfile.Instance.totalCoins = data.totalCoins; 
            PlayerProfile.Instance.enemyUpgradePoints = data.enemyUpgradePoints; 
         
        }

        if (EnemyTreeManager.Instance != null)
        {
            EnemyTreeManager.Instance.totalHpMultiplier = data.enemyHpMul; 
            EnemyTreeManager.Instance.totalDmgMultiplier = data.enemyDmgMul; 
        }

        foreach (var skill in allSkills)
        {
            skill.isUnlocked = data.unlockedSkillNames.Contains(skill.name);
            if (skill.isUnlocked)
            {
                if (skill.type == SkillNodeSO.SkillType.BonusDamage)
                {
                    PlayerProfile.Instance.bonusDamage += skill.statValue; 
            }
                else if (skill.type == SkillNodeSO.SkillType.BonusHealth)
                {
                    PlayerProfile.Instance.bonusHealth += skill.statValue; 
            }
            }
        }
        if (CoreBase.Instance != null)
        {
            CoreBase.Instance.UpdateMaxHealth(PlayerProfile.Instance.bonusHealth);
        }

        RefreshAllUI();
        Debug.Log("โหลดข้อมูลทั้งหมดสำเร็จ!");
    }
    private void RefreshAllUI()
    {

        SkillButtonUI[] buttons = GameObject.FindObjectsOfType<SkillButtonUI>();
        foreach (var btn in buttons) btn.UpdateVisual();
        EnemyNodeUI[] enemyButtons = GameObject.FindObjectsOfType<EnemyNodeUI>();
        foreach (var btn in enemyButtons) btn.UpdateVisual(); 

    }

    public void ResetOnlySkills()
    {
        int refundAmount = 0;


        foreach (var skill in allSkills)
        {
            if (skill.isUnlocked)
            {
                refundAmount += skill.cost;
                skill.isUnlocked = false;
            }
        }

        PlayerProfile.Instance.bonusDamage = 0;
        PlayerProfile.Instance.bonusHealth = 0;


        if (CoreBase.Instance != null)
        {
            CoreBase.Instance.UpdateMaxHealth(0);
        }

        PlayerProfile.Instance.totalCoins += refundAmount;

        RefreshAllUI();

        Debug.Log($"Reset สำเร็จ! คืนเงิน: {refundAmount} และล้างโบนัส Stat ทั้งหมดแล้ว");
    }
    public void ResetEnemyTreeOnly()
    {
         foreach (var eNode in allEnemySkills) 
        {
            eNode.isUnlocked = false;
        }

        PlayerProfile.Instance.enemyUpgradePoints = PlayerProfile.Instance.playerLevel / 5;

       
        EnemyTreeManager.Instance.totalHpMultiplier = 1f;
        EnemyTreeManager.Instance.totalDmgMultiplier = 1f;

        RefreshAllUI();
        Debug.Log("รีเซ็ต Enemy Tree สำเร็จ! คืนแต้มอัปเกรดทั้งหมด");
    }
}