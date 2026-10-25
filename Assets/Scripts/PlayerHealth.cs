using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //health variables
    public float maxHealth = 100f;
    public float currentHealth;
    public bool IsDead { get; private set; }
    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    //player spawn
    private void Awake()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    //player taking damage
    public void TakeDamage(float damage)
    {
        if (IsDead || damage <= 0f) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    //player healing
    public void Heal(float amount)
    {
        if (IsDead || amount <= 0f) return;

        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    //player death
    private void Die(){
        IsDead = true;
        OnDeath?.Invoke();

        Debug.Log("Player has died.");
    }
}
