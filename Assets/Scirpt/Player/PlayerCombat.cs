using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public GameObject slashEffectPrefab; 
    public float minSwipeDistance = 0.2f; 
    private Vector2 startPos;
    private Camera mainCam;

    public float stunDuration = 1.5f;
    private float stunTimer = 0f;
    public bool isStunned => stunTimer > 0;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            return;
        }
        if (Pointer.current == null) return;

        if (Pointer.current.press.wasPressedThisFrame)
        {
            startPos = mainCam.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        }

      
        if (Pointer.current.press.wasReleasedThisFrame)
        {
            Vector2 endPos = mainCam.ScreenToWorldPoint(Pointer.current.position.ReadValue());
            float distance = Vector2.Distance(startPos, endPos);

            if (distance >= minSwipeDistance)
            {
                CreateSlash(startPos, endPos);
            }
        }
    }

    void CreateSlash(Vector2 start, Vector2 end)
    {
     
        Vector2 midPoint = (start + end) / 2f;

        Vector2 dir = end - start;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

  
        GameObject slash = Instantiate(slashEffectPrefab, midPoint, Quaternion.Euler(0, 0, angle));

   
        float swipeLength = dir.magnitude;
        slash.transform.localScale = new Vector3(swipeLength, 1, 1);

        SlashDetector detector = slash.GetComponent<SlashDetector>();
        if (detector != null)
        {
            detector.playerCombat = this;
        }

        Destroy(slash, 0.2f);
    }
    public void ApplyStun()
    {
        Debug.Log("Miss! Player is stunned.");
        stunTimer = stunDuration;
        
    }
}