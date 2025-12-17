using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();

        if (enemy != null)
        {
            if (!collision.isTrigger)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}