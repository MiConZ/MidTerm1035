using UnityEngine;
using TMPro; 

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText; 

    void Update()
    {
      
        if (PlayerProfile.Instance != null && moneyText != null)
        {
            moneyText.text = PlayerProfile.Instance.totalCoins.ToString("N0");
           
        }
    }
}
