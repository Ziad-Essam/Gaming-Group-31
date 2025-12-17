using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerZ: MonoBehaviour
{
    public float maxSpeed = 2;
    public int damage = 1;
    public SpriteRenderer sr;

    public int maxHealth = 100;
    public int currentHealth;

    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    public void flip()
    {
        sr.flipX = !sr.flipX;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Hurt");
        }

        Debug.Log(gameObject.name + " took damage! HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " Died!");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(FindObjectOfType<PlayerStats>() != null) 
            {
               FindObjectOfType<PlayerStats>().TakeDamage(damage);
            }
            
        }
        else if (other.tag == "Wall")
        {
            flip(); 
        }
    }
}