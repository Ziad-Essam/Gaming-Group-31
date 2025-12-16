using UnityEngine;

public class CollectTeleport : MonoBehaviour
{
    public AudioClip TPsound;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            // 1. Get the specific script attached to the object we hit
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null)
            {
                // Accessing STATIC variables (Shared)
                PlayerStats.health = 100;
                PlayerStats.lives = 3;

                // Accessing NON-STATIC variable (Specific to this player object)
                stats.hasTeleport = true;

                AudioManager.Instance.PlayMusicSFX(TPsound);
                Debug.Log("Lives: " + PlayerStats.lives);
                Destroy(gameObject);
            }
        }
    }
}