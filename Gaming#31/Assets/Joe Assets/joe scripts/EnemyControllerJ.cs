using UnityEngine;

public class EnemyControllerJ : MonoBehaviour
{
    public float maxSpeed = 2f;
    public int damage = 1;
    public SpriteRenderer sr;

    public int maxHealth = 100;
    public int currentHealth;

    protected Animator anim;
    protected bool isDead = false;
    protected Rigidbody2D rb; // Declared here for consistency

    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void flip()
    {
        if (sr != null)
            sr.flipX = !sr.flipX;
    }

    public virtual void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;

        // REMOVED: anim.SetTrigger("Hurt"); -- This is now handled by the derived classes (Witch/Shadow)

        Debug.Log(gameObject.name + " took damage! HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Base Die() is virtual, allowing specific enemies to override the animation/destruction timing.
    public virtual void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log(gameObject.name + " died!");

        // Default behavior for simple enemies
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            // Note: FindObjectOfType<PlayerStats>() is usually slow; consider a direct reference if possible.
            PlayerStats player = FindObjectOfType<PlayerStats>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
        else if (other.CompareTag("Wall"))
        {
            flip();
        }
    }
}