using UnityEngine;

public class RespawnsEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxSpawns = 3;
    public float spawnSpacing = 1.5f; // distance between enemies

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < maxSpawns; i++)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.x += i * spawnSpacing;

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }

        GetComponent<Collider2D>().enabled = false;
    }
}
