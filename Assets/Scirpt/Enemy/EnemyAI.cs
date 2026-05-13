using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyData data;
    private float currentSpeed;
    private float currentHealth; 
    private Transform target;
    private SpriteRenderer spriteRenderer;

    private int scaledDamage;

    [Header("Effects")]
    public GameObject smokeEffectPrefab;
    void Start()
    {
        if (data == null)
        {
            Debug.LogError("กรุณาลากไฟล์ Data SO ใส่ใน Inspector ด้วยครับ!"); 
            return;
        }

      
        currentHealth = Mathf.RoundToInt(data.maxHealth * EnemyTreeManager.Instance.totalHpMultiplier); 
      
        currentSpeed = data.moveSpeed; 

      
        scaledDamage = Mathf.RoundToInt(data.attackDamage * EnemyTreeManager.Instance.totalDmgMultiplier); 

        target = GameObject.Find("CoreBase")?.transform; 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
      
        if (EnemyTreeManager.Instance.totalHpMultiplier > 1.0f)
        {
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, 0.2f);
        }
    }

    void Update()
    {
        if (target != null)
        {

            if (target != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);
                FlipTowardsTarget();
            }
        }

       
    }
    void SpawnSmokeEffect()
    {
       
        if (smokeEffectPrefab != null)
        {
           
            GameObject smoke = Instantiate(smokeEffectPrefab, transform.position, Quaternion.identity);

            
            Destroy(smoke, 0.2f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CoreBase") || other.name == "CoreBase")
        {
            CoreBase baseScript = other.GetComponent<CoreBase>(); 
            if (baseScript != null)
            {
                baseScript.TakeDamage(scaledDamage); 
            }
            SpawnSmokeEffect();
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; 

        spriteRenderer.color = Color.red; 
        Invoke("ResetColor", 0.1f); 
        if (currentHealth <= 0)
        {
            if (PlayerProfile.Instance != null)
            {
                PlayerProfile.Instance.AddRewards(data.coinDrop, data.expValue); 
            }

            // หากยังมี ScoreManager อยู่ให้เก็บไว้ได้ หรือจะย้ายไปใช้ระบบอื่นก็ได้ครับ
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(data.expValue);
            }
            SpawnSmokeEffect();
            Destroy(gameObject); 
        }
    }
    void FlipTowardsTarget()
    {
        
        if (target.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false; 
        }
      
        else if (target.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true; 
        }
    }
    
}