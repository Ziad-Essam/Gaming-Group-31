using UnityEngine;

public class RespawnsEnemy : MonoBehaviour
{
    public Transform enemy;
    public Transform spawnPoint;

    private bool hasRespawned = false; // 🔒 ADD THIS

    void RespawnEnemy()
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 🔒 if already respawned, do nothing
        if (hasRespawned)
            return;

        if (other.CompareTag("Player"))
        {
            RespawnEnemy();
            hasRespawned = true; // 🔒 IMPORTANT
        }
    }
}
