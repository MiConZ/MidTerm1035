using UnityEngine;

public class SlashDetector : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource; 
    public AudioClip SlashSound;

    [HideInInspector] public PlayerCombat playerCombat;
    private bool hasHit = false; 


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            hasHit = true;
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (enemy != null)
            {
               
             
                int currentDamage = ScoreManager.Instance.currentDamage;
                enemy.TakeDamage(currentDamage);

                Debug.Log("Player slashed enemy with " + currentDamage + " damage!");
            }
        }
    }
    private void OnDestroy()
    {
        if (audioSource != null && SlashSound != null)
        {
            audioSource.PlayOneShot(SlashSound);
        }
        if (!hasHit && playerCombat != null)
        {
            playerCombat.ApplyStun();
        }
    }
}