using UnityEngine;

public class WeaponDamageZ : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyControllerZ enemy = collision.GetComponent<EnemyControllerZ>();

        if (enemy != null)
        {
            if (!collision.isTrigger)
            {
                enemy.TakeDamage(damage);
            }
        }



    }
}