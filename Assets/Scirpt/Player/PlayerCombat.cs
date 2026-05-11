using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerCombat : MonoBehaviour
{
    public GameObject slashEffectPrefab; 
    public float minSwipeDistance = 0.2f; 
    private Vector2 startPos;
    private Camera mainCam;

    public float stunDuration = 1.5f;
    private float stunTimer = 0f;
    public bool isStunned => stunTimer > 0;

   
    [Header("Orange Buff Skill Settings")]
    public float skillDuration = 10f;  
    public float skillCooldown = 30f;  
    public float damageMultiplier = 1.5f;
    public Text cooldownTextFireslash;
    private float skillEndTime;
    private float nextSkillAvailableTime;

   
    public bool isSlashBuffActive => Time.time < skillEndTime;
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

        //[Header("Orange Buff Skill Settings")]

        if (cooldownTextFireslash != null)
        {

            if (Time.time < nextSkillAvailableTime)
            {

                if (!cooldownTextFireslash.gameObject.activeSelf)
                {
                    cooldownTextFireslash.gameObject.SetActive(true);
                }


                float remainingTime = nextSkillAvailableTime - Time.time;
                 cooldownTextFireslash.text = Mathf.Ceil(remainingTime).ToString();
            }
            else
            {
                SpriteRenderer sr = slashEffectPrefab.GetComponentInChildren<SpriteRenderer>();

                if (sr != null)
                {

                    sr.color = new Color(0f, 0f, 1f);
                }
                if (cooldownTextFireslash.gameObject.activeSelf)
                {
                    cooldownTextFireslash.gameObject.SetActive(false);
                }
            }
        }
    }
    public void ActivateSlashBuffSkill()
    {

        if (Time.time >= nextSkillAvailableTime)
        {
          
            skillEndTime = Time.time + skillDuration;
            nextSkillAvailableTime = Time.time + skillCooldown;


            SpriteRenderer sr = slashEffectPrefab.GetComponentInChildren<SpriteRenderer>();

            if (sr != null)
            {
                
                sr.color = new Color(1f, 0.5f, 0f);
            }
            Debug.Log("Activated Orange Buff Skill!");
        }
        else
        {

            Debug.Log($"Skill on cooldown. {Mathf.Ceil(nextSkillAvailableTime - Time.time)}s remaining.");
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