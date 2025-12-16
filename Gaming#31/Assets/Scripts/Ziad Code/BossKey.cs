using UnityEngine;

public class BossKey : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Find the PlayerStats script
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null)
            {
                // 2. Give the key to the player
                stats.hasBossKey = true;
                stats.hasTeleport = true;
                Debug.Log("Key Picked Up!");
                Destroy(gameObject); // Destroy the physical key object
            }
        }
    }
}