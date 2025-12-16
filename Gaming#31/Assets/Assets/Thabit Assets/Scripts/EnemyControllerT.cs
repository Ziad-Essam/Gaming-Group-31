using System.Collections;
using UnityEngine;

public class EnemyControllerT : MonoBehaviour
{
    protected bool isDead = false;

    [Header("Movement & Stats")]
    public float maxSpeed = 2f;
    public int damage = 1;
    public SpriteRenderer sr;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Death Settings")]
    public float deathAnimDuration = 3.0f; // Set this to match your death animation length

    // Cached components
    protected Animator anim;
    protected Rigidbody2D rb;
    private PlayerStats player;

    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        player = FindObjectOfType<PlayerStats>();
    }

    // Flip the sprite
    public void flip()
    {
        if (sr != null)
            sr.flipX = !sr.flipX;
    }

    // Take damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Play hurt animation if exists
        if (anim != null)
            anim.SetTrigger("hurt");

        Debug.Log(gameObject.name + " took damage! HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Handle death
    public virtual void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}

