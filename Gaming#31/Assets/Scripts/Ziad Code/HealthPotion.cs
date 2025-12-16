using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Increase the Limit (100 -> 200 -> 300)
            PlayerStats.maxHealth += 100;

            // 2. Heal to the new Full Limit
            PlayerStats.health = PlayerStats.maxHealth;
            
            Debug.Log("Max HP upgraded! New Health: " + PlayerStats.health);
            
            Destroy(gameObject);
        }
    }
}