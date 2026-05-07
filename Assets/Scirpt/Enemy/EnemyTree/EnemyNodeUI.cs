using UnityEngine;
using UnityEngine.UI;

public class EnemyNodeUI : MonoBehaviour
{
    public EnemySkillNodeSO nodeData; 
    public Image buttonImage;
    public Button myButton;

    [Header("Color Settings")]
    public Color lockedColor = Color.gray;
    public Color availableColor = Color.white;
    public Color unlockedColor = Color.red; 

    void Start()
    {
        UpdateVisual();

        
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnNodeClick);
        }
    }

    public void UpdateVisual()
    {
        if (nodeData == null) return;

        if (nodeData.isUnlocked)
        {
            buttonImage.color = unlockedColor;
            myButton.interactable = false; 
        }
        else if (IsAvailable())
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

   
    private bool IsAvailable()
    {
        // ถ้าไม่มีโหนดก่อนหน้า ก็พร้อมอัป
        if (nodeData.prerequisites == null || nodeData.prerequisites.Count == 0) return true;

        // เช็กว่าโหนดก่อนหน้าทั้งหมดถูกปลดล็อกหรือยัง
        foreach (var pre in nodeData.prerequisites)
        {
            if (!pre.isUnlocked) return false;
        }
        return true;
    }

    private void OnNodeClick()
    {
        // เรียกใช้ฟังก์ชันอัปเกรดจาก EnemyTreeManager
        if (EnemyTreeManager.Instance != null)
        {
            EnemyTreeManager.Instance.UpgradeEnemy(nodeData); 
            UpdateVisual(); 
        }
    }
}