using UnityEngine;
using System.Collections.Generic;

public class SwordDamage : MonoBehaviour
{
    private Collider swordCollider;
    public float damage = 25f; // Damage value for the sword
    private bool canDamage = false; // Flag to determine if the sword can deal damage

    // HashSet to keep track of bosses that have already been hit during the current attack
    private HashSet<BossHealth> hitBosses = new HashSet<BossHealth>();

    void Awake()
    {
        swordCollider = GetComponent<Collider>();
        swordCollider.enabled = false; // Disable the sword collider initially
    }





    // Method to enable damage dealing
    public void EnableDamage()
    {
        canDamage = true;
        hitBosses.Clear(); // Clear the set of hit bosses at the start of each attack
        swordCollider.enabled = true; // Enable the sword collider when damage is enabled
    }

    // Method to disable damage dealing
    public void DisableDamage()
    {
        canDamage = false;
        swordCollider.enabled = false; // Disable the sword collider when damage is disabled
    }






    // Method called when the sword collides with another collider
    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return; // If damage is not enabled, do nothing
        {
            BossHealth boss = other.GetComponentInParent<BossHealth>();
            if (boss != null && !hitBosses.Contains(boss))
            {
                hitBosses.Add(boss); // Add the boss to the set of hit bosses
                boss.TakeDamage(damage); // Call the TakeDamage method on the boss

                Debug.Log("Sword hit boss");
            }
        }
    }
}
