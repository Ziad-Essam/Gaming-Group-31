using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyZ : MonoBehaviour
{
    public Transform enemy;
    public Transform spawnPoint;
    
    public int enemyCount = 15; 

    public float spreadRange = 2f; 

    void RespawnEnemey()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-spreadRange, spreadRange), 
                Random.Range(-spreadRange, spreadRange), 
                0 
            );

            Vector3 finalPosition = spawnPoint.position + randomOffset;

            Instantiate(enemy, finalPosition, Quaternion.identity);
        }
    }
    
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           RespawnEnemey();
           
        }
    }
}