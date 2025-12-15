using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [Header("Barrier Settings")]
    public GameObject barrier; // The invisible wall that appears
    public bool lockPlayerIn = true; // Should the barrier trap the player inside?

    private bool hasTriggered = false;

    private void Start()
    {
        // Make sure barrier starts disabled
        if (barrier != null)
            barrier.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if player entered
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            
            // Activate the barrier
            if (barrier != null)
            {
                barrier.SetActive(true);
                Debug.Log("Boss room locked!");
            }
        }
    }

    // Optional: Call this when boss dies to unlock the room
    public void UnlockRoom()
    {
        if (barrier != null)
        {
            barrier.SetActive(false);
            Debug.Log("Boss room unlocked!");
        }
    }
}