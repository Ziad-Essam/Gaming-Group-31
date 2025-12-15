using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
             // Assuming your player has a script called 'PlayerStats'
             PlayerStats player = collision.GetComponent<PlayerStats>();
             if(player != null)
             {
                 player.TakeDamage(damage);
             }
        }
    }
}