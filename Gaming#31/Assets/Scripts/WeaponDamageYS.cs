using UnityEngine;

public class WeaponDamageYS : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyControllerYS enemy = collision.GetComponentInParent<EnemyControllerYS>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
