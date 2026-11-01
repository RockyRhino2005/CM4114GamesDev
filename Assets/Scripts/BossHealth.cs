using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 500f;
    private float currentHealth;
    private bool isDead;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        
        Debug.Log("Boss Health: " + currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Boss has been defeated.");
    }
}
