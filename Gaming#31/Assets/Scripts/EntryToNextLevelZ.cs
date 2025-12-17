using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryToNextLevelZ : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null && stats.hasTeleport == true)
            {
                int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

                SceneManager.LoadScene(currentLevelIndex + 1);
                
                Debug.Log("Moved to next level index: " + (currentLevelIndex + 1));
                
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