using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.maxHealth += 100;

            PlayerStats.health = PlayerStats.maxHealth;
            
            Debug.Log("Max HP upgraded! New Health: " + PlayerStats.health);
            
            Destroy(gameObject);
        }
    }
}