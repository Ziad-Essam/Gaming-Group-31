using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 2f;
    public int damage = 1;
    public int health = 50;

    public SpriteRenderer sr;
    public Animator anim;
    protected Rigidbody2D rb;

    protected bool dead = false;

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

    // PLAYER TOUCH
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (dead) return;

        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Attacking");

            PlayerStats ps = other.GetComponent<PlayerStats>();
            if (ps != null)
                ps.TakeDamage(damage);
        }
        else if (other.CompareTag("Wall"))
        {
            Flip();
        }
    }

    // CALLED BY WEAPON
    public void TakeDamage(int dmg)
    {
        if (dead) return;

        health -= dmg;
        anim.SetTrigger("Hit");

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        dead = true;
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false); // default death
    }
}
