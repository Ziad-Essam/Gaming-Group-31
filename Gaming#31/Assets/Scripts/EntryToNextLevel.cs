using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryToNextLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            // Check if player has the ability required to pass
            if (stats != null && stats.hasTeleport == true)
            {
                // --- GENERAL AUTOMATIC LOADING ---
                // 1. Get the number (index) of the current level
                int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

                // 2. Load the next number in the list
                SceneManager.LoadScene(currentLevelIndex + 1);

                Debug.Log("Moved to next level index: " + (currentLevelIndex + 1));

                // Keep audio logic
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayMusic(AudioManager.Instance.caveMusic);
                }
            }
            else
            {
                Debug.Log("Access Denied!");
                FindObjectOfType<LevelManager>().RespawnPlayer();
            }
        }
    }
} 
