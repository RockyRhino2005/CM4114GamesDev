using UnityEngine;
using UnityEngine.AI;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 500f;
    private float currentHealth;
    private bool isDead;

    public float deathLenght = 5f;

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

        BossAi bossAi = GetComponent<BossAi>();
        if (bossAi != null)
        {
            bossAi.enabled = false;
        }
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Die");
            animator.SetBool("IsWalking", false);
        }
        
        Destroy(gameObject, deathLenght);
}
}