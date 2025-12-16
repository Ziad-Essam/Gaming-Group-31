using UnityEngine;

public class EnemyControllerYB : MonoBehaviour
{
    [Header("Stats")]
    public float maxSpeed = 2f;
    public int damage = 1;
    public int health = 50;

    [Header("Components")]
    public SpriteRenderer sr;
    public Animator anim;
    protected Rigidbody2D rb;

    protected bool isDead = false;

    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Flip()
    {
        sr.flipX = !sr.flipX;
    }

    // ENEMY TOUCHES PLAYER
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Attack");

            PlayerStats ps = other.GetComponent<PlayerStats>();
            if (ps != null)
                ps.TakeDamage(damage);
        }
        else if (other.CompareTag("Wall"))
        {
            Flip();
        }
    }

    // CALLED BY PLAYER WEAPON
    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        health -= dmg;
        anim.SetTrigger("Hit");

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
