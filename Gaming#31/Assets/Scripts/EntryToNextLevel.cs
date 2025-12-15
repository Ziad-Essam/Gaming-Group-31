using UnityEngine;
using UnityEngine.SceneManagement; // 1. Added this line

public class EntryToNextLevel : MonoBehaviour
{
    // 2. Deleted "public Scene SceneManager;" - You don't need it!

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerStats.hasTeleport == true)
        {
            SceneManager.LoadScene(3);
            Debug.Log("Lives: " + PlayerStats.lives);
            Debug.Log("HP: " + PlayerStats.health);
            Debug.Log("Score: " + PlayerStats.score);
            AudioManager.Instance.PlayMusic(AudioManager.Instance.caveMusic);
            
        }
        else
        {
            FindObjectOfType<LevelManager>().RespawnPlayer();
        }
    }
}