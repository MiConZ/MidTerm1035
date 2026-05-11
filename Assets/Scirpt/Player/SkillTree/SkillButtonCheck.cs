using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SkillButtonCheck : MonoBehaviour
{
    private string savePath;
    public GameObject FireSlash;
    public GameObject Explosion;
    public GameObject DoubleSlash;
    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "player_save.json");
    }
    public void CheckMyThreeSkills()
    {
       
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("ไม่มีไฟล์เซฟ");
            return;
        }

        string jsonContent = File.ReadAllText(savePath);
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(jsonContent);
        List<string> mySkills = data.unlockedSkillNames;

        
        bool hasDoubleSlash = mySkills.Contains("DoubleSlash");
        bool hasExplosion = mySkills.Contains("Explosion");
        bool hasFireSlash = mySkills.Contains("FireSlash");

        Debug.Log("มี DoubleSlash ไหม? : " + hasDoubleSlash);
        Debug.Log("มี Explosion ไหม? : " + hasExplosion);
        Debug.Log("มี FireSlash ไหม? : " + hasFireSlash);
        if(hasDoubleSlash)
        {
            DoubleSlash.gameObject.SetActive(true);
        }
        else
        {
            DoubleSlash.gameObject.SetActive(false);
        }
        if (hasExplosion)
        {
            Explosion.gameObject.SetActive(true);
        }
        else
        {
            Explosion.gameObject.SetActive(false);
        }
        if (hasFireSlash)
        {
            FireSlash.gameObject.SetActive(true);
        }
        else
        {
            FireSlash.gameObject.SetActive(false);
        }

        if (hasDoubleSlash && hasExplosion && hasFireSlash)
        {
            Debug.Log("✅ ผู้เล่นปลดล็อกครบทั้ง DoubleSlash, Explosion และ FireSlash แล้ว!");
            }
            
        else
        {
            Debug.Log("❌ ยังมีสกิลในกลุ่มนี้ไม่ครบ");
        }
    }
     void Start()
    {
        CheckMyThreeSkills();
    }

}
