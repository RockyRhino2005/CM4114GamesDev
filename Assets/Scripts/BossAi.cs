using UnityEngine;
using UnityEngine.AI;

public class BossAi : MonoBehaviour
{
   public Transform player;
   private float attackRange = 10f;
   private float attackCooldown = 2f;

    private NavMeshAgent agent;
    private Animator animator;
    private float nextAttackTime;
    private int attackNumber = 1;
    private float attackEndTime;


    //get components
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        agent.stoppingDistance = attackRange;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 directionToPlayer = player.position - transform.position; directionToPlayer.y = 0f;

        float distance = directionToPlayer.magnitude;

        //wait until current attack ends
        if (Time.time < attackEndTime)
        {
            return;
        }

        // Debug.Log("Boss distance: " + distance);
        //chase player if in range
        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            //stop moving and face player
            agent.isStopped = true;
            agent.ResetPath();
            animator.SetBool("IsWalking", false);

            Vector3 direction = (player.position - transform.position);
            direction.y = 0;

            //quaternion is for rotation
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
            }

            
            //attack player if cooldown is over
            if (Time.time >= nextAttackTime)
            {
                Attack();
            }
            
        }
    }

    void Attack()
    {
        //trigger attack animation
        animator.SetTrigger("Attack" + attackNumber);

        attackNumber++;

        //cycle through attacks
        attackNumber++;
        if (attackNumber > 3)
        {
            attackNumber = 1;
        }

        //Time for attack animation
        attackEndTime = Time.time + 1.5f;
        nextAttackTime = Time.time + attackCooldown;
    }
}
