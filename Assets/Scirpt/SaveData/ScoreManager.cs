using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text WaveText;
    public int score = 0;
    public int enemyKilledCount = 0;
    public Text scoreText;
    public int playerBaseDamage = 10; 
    public int currentDamage;

    [Header("Difficulty Settings")]
    public int difficultyLevel = 1;
    public float speedMultiplier = 1.1f; 
    public int healthBonus = 1;          
    void Awake()
    {
        Instance = this;
        currentDamage = playerBaseDamage;
    }

    public void AddScore(int amount)
    {
        score += amount;
        enemyKilledCount++;

      
        if (enemyKilledCount % 10 == 0)
        {
            IncreaseDifficulty();
            enemyKilledCount = 0;
        }

        UpdateUI();
    }

    void IncreaseDifficulty()
    {
        difficultyLevel++;
        UpdateUI();

        Debug.Log("Level Up! Difficulty: " + difficultyLevel);
        
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text =  "Score: " + score;
        }
        if(WaveText != null)
        {
            WaveText.text = "Wave " + difficultyLevel;
        }
        
    }
}