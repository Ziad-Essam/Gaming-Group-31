using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Medusa : EnemyControllerZ
{
    [Header("Weapon Setup")]
    public GameObject medusaProjectile; 
    public Transform firePoint;         

    [Header("AI Settings")]
    public float chaseRange = 8f;       
    public float attackRange = 4f;      
    public float attackCooldown = 2f;

    private Transform player;
    private float nextAttackTime = 0;
    
    // VARIABLES
    private Animator anim;
    private Rigidbody2D rb;
    private bool isAttacking = false; 

    public override void Start()
    {
        base.Start(); 
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;
        
        // Don't rotate or move while actually throwing the projectile (mid-animation)
        if (isAttacking) 
        {
            rb.linearVelocity = Vector2.zero;
            return; 
        }

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        // 1. ATTACK RANGE
        if (distToPlayer < attackRange)
        {
            rb.linearVelocity = Vector2.zero; // Stop moving
            anim.SetBool("IsRunning", false);

            // FIX: Face the player even while standing still!
            LookAtPlayer();

            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(PerformAttack());
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        // 2. CHASE RANGE
        else if (distToPlayer < chaseRange)
        {
            // Reset trigger to prevent stuck animations
            anim.ResetTrigger("Attack"); 
            
            // Move and Look
            ChasePlayer();
            anim.SetBool("IsRunning", true);
        }
        // 3. IDLE
        else
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("IsRunning", false);
        }
    }

    // --- NEW HELPER FUNCTION ---
   void LookAtPlayer()
    {
        // Player is to the RIGHT
        if (transform.position.x < player.position.x)
        {
            // Face Right (0 rotation)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // Player is to the LEFT
        else
        {
            // Face Left (180 rotation)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void ChasePlayer()
    {
        LookAtPlayer();

        // Ensure movement matches the rotation we just set
        if (transform.position.x < player.position.x)
        {
            // Move Right (Positive speed)
            rb.linearVelocity = new Vector2(maxSpeed, rb.linearVelocity.y);
        }
        else
        {
            // Move Left (Negative speed)
            rb.linearVelocity = new Vector2(-maxSpeed, rb.linearVelocity.y);
        }
    }

  IEnumerator PerformAttack()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        
        yield return new WaitForSeconds(0.3f); 
        
        if(medusaProjectile != null && firePoint != null)
        {
            // CHANGE THIS LINE:
            // Use firePoint.rotation so the bullet spawns facing the same way as the Medusa
            Instantiate(medusaProjectile, firePoint.position, firePoint.rotation);
        }

        yield return new WaitForSeconds(0.5f); 
        
        isAttacking = false;
    }
}