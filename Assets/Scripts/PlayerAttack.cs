using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    public SwordDamage swordDamage; // Reference to the SwordDamage script

    //gets components
    void Start()
    {
        animator = GetComponent<Animator>();
    }





    void Update()
    {
        //inputs for the attack and spin attack

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




    //attack
    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    //spin attack
    void Spin()
    {
        animator.SetTrigger("SpinAttack");
    }





    //triggers animation events to enable sword damage during the attack animations
    public void StartAttack()
    {
        swordDamage.EnableDamage();
        Debug.Log("Sword damage enabled");
    }

    public void EndAttack()
    {
        swordDamage.DisableDamage();
        Debug.Log("Sword damage disabled");
    }
}