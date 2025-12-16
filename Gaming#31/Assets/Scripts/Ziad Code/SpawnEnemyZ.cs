using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyZ : MonoBehaviour
{
    public Transform enemy;
    public Transform spawnPoint;
    
    // How many enemies do you want?
    public int enemyCount = 15; 

    // How far apart should they spawn?
    public float spreadRange = 2f; 

    void RespawnEnemey()
    {
        // This loop runs 15 times (or whatever enemyCount is)
        for (int i = 0; i < enemyCount; i++)
        {
            // 1. Calculate a random position near the spawn point
            Vector3 randomOffset = new Vector3(
                Random.Range(-spreadRange, spreadRange), // Random X
                Random.Range(-spreadRange, spreadRange), // Random Y
                0 // Keep Z at 0 for 2D
            );

            Vector3 finalPosition = spawnPoint.position + randomOffset;

            // 2. Spawn the enemy at that new random position
            Instantiate(enemy, finalPosition, Quaternion.identity);
        }
    }
    
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           RespawnEnemey();
           
           // Optional: Destroy the spawner so it doesn't trigger again
           // Destroy(gameObject); 
        }
    }
}