using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Look for 'EnemeyController' (Matches your specific script name)
        EnemyController enemy = collision.GetComponent<EnemyController>();

        if (enemy != null)
        {
            // 2. Ignore the 'Trigger' collider (the vision cone)
            // Only damage if we hit the solid body (isTrigger == false)
            if (!collision.isTrigger)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}