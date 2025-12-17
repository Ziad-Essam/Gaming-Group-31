using UnityEngine;

public class CollectTeleport : MonoBehaviour
{
    public AudioClip TPsound;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null)
            {
                PlayerStats.health = 100;
                PlayerStats.lives = 3;

                stats.hasTeleport = true;

                AudioManager.Instance.PlayMusicSFX(TPsound);
                Debug.Log("Lives: " + PlayerStats.lives);
                Destroy(gameObject);
            }
        }
    }
}