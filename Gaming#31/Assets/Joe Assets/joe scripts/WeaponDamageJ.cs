using UnityEngine;

public class WeaponDamageJ : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Look for 'EnemeyController'
        EnemyControllerJ enemy = collision.GetComponent<EnemyControllerJ>();

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