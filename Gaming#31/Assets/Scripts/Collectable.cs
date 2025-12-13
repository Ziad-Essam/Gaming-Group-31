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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
