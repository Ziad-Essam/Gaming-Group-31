
using UnityEngine;

public class CollectTeleport : MonoBehaviour
{

    public AudioClip TPsound;

void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerStats.health = 100;
            PlayerStats.hasTeleport = true;
            PlayerStats.lives = 3;
            AudioManager.Instance.PlayMusicSFX(TPsound);
            Debug.Log("Lives: " + PlayerStats.lives);
            Destroy(gameObject);

        }

    }
   // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
