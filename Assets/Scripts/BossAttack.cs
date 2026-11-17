using UnityEngine;
using System.Collections.Generic;

public class BossAttack : MonoBehaviour
{
    //gets the boss colliders and enables and disables them when attacking
    public float damage = 15f;

    private Collider fistCollider;
    private bool canDamage;

    private HashSet<PlayerHealth> hitPlayers = new HashSet<PlayerHealth>();

    void Awake()
    {
        fistCollider = GetComponent<Collider>();
        fistCollider.enabled = false;
    }

    public void EnableDamage()
    {
        hitPlayers.Clear();
        canDamage = true;
        fistCollider.enabled = true;
    }

    public void DisableDamage()
    {
        canDamage = false;
        fistCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return;

        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();

        if (player != null && !hitPlayers.Contains(player))
        {
            hitPlayers.Add(player);
            player.TakeDamage(damage);

            Debug.Log("Boss punched player!");
        }
    }
}
