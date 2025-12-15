using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 2;
    public int damage = 1;
    public SpriteRenderer sr;
    public Rigidbody2D rb;

    public int maxHealth = 100;
    public int currentHealth;

    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void flip()
    {
        sr.flipX = !sr.flipX;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // --- NEW CODE START ---
        // Play the Hurt animation if we have one
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Hurt");
        }
        // --- NEW CODE END ---

        Debug.Log(gameObject.name + " took damage! HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " Died!");
        // Play death animation here if you have one later!
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // We still want to damage the player if we touch them
            if (FindObjectOfType<PlayerStats>() != null)
            {
                FindObjectOfType<PlayerStats>().TakeDamage(damage);
            }

            // DELETE THIS LINE: flip();  <--- This was causing the bug!
        }
        else if (other.tag == "Wall")
        {
            flip(); // Keep this! We still want to flip if we hit a wall.
        }
    }
}