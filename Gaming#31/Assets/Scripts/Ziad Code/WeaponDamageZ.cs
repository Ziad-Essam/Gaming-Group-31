using UnityEngine;

public class WeaponDamageZ : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Look for 'EnemeyController' (Matches your specific script name)
        EnemyControllerZ enemy = collision.GetComponent<EnemyControllerZ>();

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