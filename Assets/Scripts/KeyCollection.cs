using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    public GameObject door;
    public int totalkeys = 5;

    private int keysCollected = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            keysCollected++;
            other.enabled = false; 
            Destroy(other.gameObject);

            if (keysCollected >= totalkeys)
            {
                door.SetActive(false);
            }
        }
    }
}
