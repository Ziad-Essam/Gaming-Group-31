using UnityEngine;

public class WeaponDamageJ : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyControllerJ enemy = collision.GetComponent<EnemyControllerJ>();

        if (enemy != null)
        {
            if (!collision.isTrigger) 
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}