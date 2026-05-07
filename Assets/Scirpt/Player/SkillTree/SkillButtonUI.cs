using UnityEngine;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour
{
    public SkillNodeSO skillData;  
    public Image buttonImage;      
    public Button myButton;       
    [Header("Color Settings")]
    public Color lockedColor = Color.gray;      
    public Color availableColor = Color.white;  
    public Color unlockedColor = Color.green;   
    void Start()
    {
        UpdateVisual();
    }

   
    public void UpdateVisual()
    {
        if (skillData.isUnlocked)
        {
            buttonImage.color = unlockedColor;
            myButton.interactable = false; 
        }
        else if (skillData.IsAvailable()) 
        {
            buttonImage.color = availableColor;
            myButton.interactable = true;
        }
        else
        {
            buttonImage.color = lockedColor;
            myButton.interactable = false; 
        }
    }
}