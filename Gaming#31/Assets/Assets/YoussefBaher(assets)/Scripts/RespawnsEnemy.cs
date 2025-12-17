using UnityEngine;

public class RespawnsEnemyYB : MonoBehaviour
{
    public Transform enemy;
    public Transform spawnPoint;

    private bool hasRespawned = false; 
    void RespawnEnemy()
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasRespawned)
            return;

        if (other.CompareTag("Player"))
        {
            RespawnEnemy();
            hasRespawned = true; 
        }
    }
}
