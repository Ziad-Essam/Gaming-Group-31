using UnityEngine;

public class BossKey : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null)
            {
                stats.hasBossKey = true;
                stats.hasTeleport = true;
                Debug.Log("Key Picked Up!");
                Destroy(gameObject); 
            }
        }
    }
}