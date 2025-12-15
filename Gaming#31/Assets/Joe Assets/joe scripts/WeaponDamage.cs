using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Look for 'EnemeyController'
        EnemyController enemy = collision.GetComponent<EnemyController>();

        if (enemy != null)
        {
            // 2. FIXED: Only damage if we hit the enemy's SOLID BODY (isTrigger == false)
            // The player weapon itself is a trigger, checking what it hits.
            if (!collision.isTrigger) 
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}