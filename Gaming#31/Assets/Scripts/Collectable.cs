using UnityEngine;

public class Collectable : MonoBehaviour
{

    public AudioClip coin1;
    public AudioClip coin2;

     void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerStats.score++;
            AudioManager.Instance.PlayRandomSFX(coin1,coin2);
            Debug.Log("Score: " + PlayerStats.score);
            Destroy(gameObject);
        }

    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
