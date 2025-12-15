using UnityEngine;

public class RespawnsEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxSpawns = 3;

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
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }

        // Optional but recommended
        GetComponent<Collider2D>().enabled = false;
    }
}
