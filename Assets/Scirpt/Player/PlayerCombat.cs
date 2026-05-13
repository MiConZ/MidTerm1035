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
    [Header("Skill Settings")]
    public int ultimateDamage = 100; 
    public GameObject ultimateEffectPrefab; 

    [Header("Orange Buff Skill Settings")]
    public float skillDuration = 10f;  
    public float skillCooldown = 30f;  
    public float damageMultiplier = 1.5f;
    public Text cooldownTextFireslash;
    private float skillEndTime;
    private float nextSkillAvailableTime;

    [Header("doubleslash Skill Settings")]
    public float dsskillDuration = 10f;
    public float dskillCooldown = 30f;
    public float dsdamageMultiplier = 3f;
    public Text dscooldownTextFireslash;
    private float dsskillEndTime;
    private float dsnextSkillAvailableTime;

    [Header("Explosion Skill Settings")]
    public float ExskillDuration = 10f;
    public float ExskillCooldown = 60f;
    public float ExdamageMultiplier = 3f;
    public Text ExcooldownTextFireslash;
    private float ExskillEndTime;
    private float ExnextSkillAvailableTime;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip SoundSkill;
    public AudioClip SoundExSkill;

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
        //[Header("doubleslash Skill Settings")]
        if (dscooldownTextFireslash != null)
        {

            if (Time.time < dsnextSkillAvailableTime)
            {

                if (!dscooldownTextFireslash.gameObject.activeSelf)
                {
                    dscooldownTextFireslash.gameObject.SetActive(true);
                }


                float remainingTime = dsnextSkillAvailableTime - Time.time;
                dscooldownTextFireslash.text = Mathf.Ceil(remainingTime).ToString();
            }
            else
            {
               
                if (dscooldownTextFireslash.gameObject.activeSelf)
                {
                    dscooldownTextFireslash.gameObject.SetActive(false);
                }
            }
        }
        //[Header("Ex Skill Settings")]
        if (ExcooldownTextFireslash != null)
        {

            if (Time.time < ExnextSkillAvailableTime)
            {

                if (!ExcooldownTextFireslash.gameObject.activeSelf)
                {
                    ExcooldownTextFireslash.gameObject.SetActive(true);
                }


                float remainingTime = ExnextSkillAvailableTime - Time.time;
                ExcooldownTextFireslash.text = Mathf.Ceil(remainingTime).ToString();
            }
            else
            {

                if (ExcooldownTextFireslash.gameObject.activeSelf)
                {
                    ExcooldownTextFireslash.gameObject.SetActive(false);
                }
            }
        }

        //[Header("Ex Skill Settings")]

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
    public void ActivateExSkill()
    {
        if (audioSource != null && SoundExSkill != null)
        {
            audioSource.PlayOneShot(SoundExSkill, 0.5f);
        }
        if (Time.time >= ExnextSkillAvailableTime)
        {
          
            ExskillEndTime = Time.time + ExskillDuration;
            ExnextSkillAvailableTime = Time.time + ExskillCooldown;


          
            ExecuteScreenClear();
            Debug.Log("Activated Ex Skill!");
        }
        else
        {

            Debug.Log($"Skill on cooldown. {Mathf.Ceil(nextSkillAvailableTime - Time.time)}s remaining.");
        }
    }

    public void ExecuteScreenClear()
    {
       
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        
        if (allEnemies.Length == 0)
        {
            Debug.Log("ไม่มีศัตรูบนจอให้ทำดาเมจ!");
            return;
        }

       
        if (ultimateEffectPrefab != null)
        {
            Instantiate(ultimateEffectPrefab, Vector3.zero, Quaternion.identity);
        }

        foreach (GameObject enemyObj in allEnemies)
        {
           
            EnemyAI enemyScript = enemyObj.GetComponent<EnemyAI>();

            if (enemyScript != null)
            {
                
                enemyScript.TakeDamage(ultimateDamage);
            }
        }

        Debug.Log($"ทำดาเมจ {ultimateDamage} ใส่ศัตรูจำนวน {allEnemies.Length} ตัวเรียบร้อย!");
    }

    public void ActiveFireSlash()
    {
        if (audioSource != null && SoundSkill != null)
        {
            audioSource.PlayOneShot(SoundSkill, 0.5f);
        }
        if (Time.time >= nextSkillAvailableTime)
        {

            skillEndTime = Time.time + skillDuration;
            nextSkillAvailableTime = Time.time + skillCooldown;
        }
        else
        {

            Debug.Log($"Skill on cooldown. {Mathf.Ceil(nextSkillAvailableTime - Time.time)}s remaining.");
        }
    }
    public void ActiveDoubleSlash()
    {
        if (audioSource != null && SoundSkill != null)
        {
            audioSource.PlayOneShot(SoundSkill, 0.5f);
        }
        if (Time.time >= dsnextSkillAvailableTime)
        {

            dsskillEndTime = Time.time + dsskillDuration;
            dsnextSkillAvailableTime = Time.time + dskillCooldown;
        }
        else
        {

            Debug.Log($"Skill on cooldown. {Mathf.Ceil(dsnextSkillAvailableTime - Time.time)}s remaining.");
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