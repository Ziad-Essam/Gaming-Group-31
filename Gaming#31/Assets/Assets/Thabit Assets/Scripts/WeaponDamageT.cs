using UnityEngine;

public class WeaponDamageT : MonoBehaviour
{
    public int damage = 20;
    public bool damagesPlayer = false;
    public bool damagesEnemy = true;

   private void OnTriggerEnter2D(Collider2D other)
{

    if (damagesEnemy)
    {
        EnemyControllerT enemy = other.GetComponent<EnemyControllerT>();
        if (enemy != null && !other.isTrigger)
        {
            enemy.TakeDamage(damage);
        }
    }

    if (damagesPlayer)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
}
