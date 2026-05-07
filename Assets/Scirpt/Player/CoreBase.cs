
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UI;

public class CoreBase : MonoBehaviour
{
    public int health = 100;
    public Text healthText;
    public int maxHealth ;
    public float cooldown = 10f;
    private float nextHealTime;


    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public GameObject retryMenuUI;
    [Header("Audio Settings")]
    public AudioSource audioSource; 
    public AudioClip hitSound;
    public AudioClip healSound;
   
    public static CoreBase Instance;
    void Awake()
    {
        if (Instance == null) Instance = this; 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        originalColor = spriteRenderer.color; 
    }

    void Start()
    {
        
        UpdateUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);

        }
    }
    public void healCoreBase()
    {
        if (Time.time >= nextHealTime)
        {
            nextHealTime = Time.time + cooldown;
            health = 100;
            UpdateUI();
            if (audioSource != null && healSound != null)
            {
                audioSource.PlayOneShot(healSound);
            }
        }
    }
    public void UpdateMaxHealth(int additionalHealth)
    {
        maxHealth = additionalHealth; 
        health += maxHealth; 
        UpdateUI();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateUI();

     
        spriteRenderer.color = Color.red;
        Invoke("ResetColor", 0.15f);     
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        if (health <= 0)
        {
            GameOver();
        }
    }

    void ResetColor()
    {
        spriteRenderer.color = originalColor; 
    }

    void UpdateUI()
    {
        if (healthText != null) healthText.text = "HP: " + health;
    }

    void GameOver()
    {
       
        retryMenuUI.gameObject.SetActive(true);
        Time.timeScale = 0f;

    }
    public void OnSkillClick(SkillNodeSO clickedSkill)
    {
        if (clickedSkill.IsAvailable())
        {
            if (PlayerProfile.Instance.totalCoins >= clickedSkill.cost)
            {
                PlayerProfile.Instance.totalCoins -= clickedSkill.cost;
                clickedSkill.isUnlocked = true;
                ApplySkillBonus(clickedSkill);
                GameDataManager.Instance.SaveGame();
                UpdateUI();
            }
        }
    }

    private void ApplySkillBonus(SkillNodeSO skill)
    {
        switch (skill.type)
        {
            case SkillNodeSO.SkillType.BonusDamage:
                PlayerProfile.Instance.bonusDamage += skill.statValue;
                Debug.Log($"เพิ่มดาเมจถาวร: +{skill.statValue}");
                break;

            case SkillNodeSO.SkillType.BonusHealth:
                PlayerProfile.Instance.bonusHealth += skill.statValue;
                
                if (CoreBase.Instance != null)
                {
                    CoreBase.Instance.UpdateMaxHealth(PlayerProfile.Instance.bonusHealth);
                }
                Debug.Log($"เพิ่มเลือดฐานถาวร: +{skill.statValue}");
                break;
        }
    }
}