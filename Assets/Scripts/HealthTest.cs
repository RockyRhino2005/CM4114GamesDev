
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerHealth))]
public class HealthTest : MonoBehaviour
{
    private PlayerHealth health;

    //checks health
    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (Keyboard.current == null || health == null)
            return;

        //h key to take damage, j key to heal
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            health.TakeDamage(10f);
            Debug.Log("player took damage");
        }

        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            health.Heal(10f);
            Debug.Log("player healed");
        }
    }
}
