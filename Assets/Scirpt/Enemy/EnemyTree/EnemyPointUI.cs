using UnityEngine;
using TMPro;

public class EnemyPointUI : MonoBehaviour
{
    public TextMeshProUGUI pointText;

    void Update()
    {
        if (PlayerProfile.Instance != null && pointText != null)
        {
            
            pointText.text = "Enemy Points: " + PlayerProfile.Instance.enemyUpgradePoints; 
        }
    }
}