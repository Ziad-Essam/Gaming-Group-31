using UnityEngine;

public class FragmentPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the stats script to run the UI function
            PlayerStats stats = other.GetComponent<PlayerStats>();
            
            if (stats != null)
            {
                stats.AddFragment(); // This adds the number AND the picture
            }
            
            Destroy(gameObject);
        }
    }
}