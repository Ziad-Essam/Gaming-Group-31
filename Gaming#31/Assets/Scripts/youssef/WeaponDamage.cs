using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get enemy even if collider is a child or trigger
        EnemyController enemy = collision.GetComponentInParent<EnemyController>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
