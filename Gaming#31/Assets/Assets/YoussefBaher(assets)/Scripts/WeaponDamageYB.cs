using UnityEngine;

public class WeaponDamageYB : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyControllerYB enemy = collision.GetComponentInParent<EnemyControllerYB>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
