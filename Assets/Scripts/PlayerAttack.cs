using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the M key was pressed
        if (Keyboard.current != null &&
            Keyboard.current.mKey.wasPressedThisFrame)
        {
            Attack();
        }

        // Check if the L key was pressed
        if (Keyboard.current != null &&
            Keyboard.current.lKey.wasPressedThisFrame)
        {
            Spin();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    void Spin()
    {
        animator.SetTrigger("SpinAttack");
    }
}