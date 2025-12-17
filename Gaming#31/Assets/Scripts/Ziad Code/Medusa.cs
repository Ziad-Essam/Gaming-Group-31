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
        
        if (isAttacking) 
        {
            rb.linearVelocity = Vector2.zero;
            return; 
        }

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < attackRange)
        {
            rb.linearVelocity = Vector2.zero; 
            anim.SetBool("IsRunning", false);

            LookAtPlayer();

            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(PerformAttack());
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else if (distToPlayer < chaseRange)
        {
            anim.ResetTrigger("Attack"); 
            
            ChasePlayer();
            anim.SetBool("IsRunning", true);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("IsRunning", false);
        }
    }

   void LookAtPlayer()
    {
        if (transform.position.x < player.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void ChasePlayer()
    {
        LookAtPlayer();

        if (transform.position.x < player.position.x)
        {
            rb.linearVelocity = new Vector2(maxSpeed, rb.linearVelocity.y);
        }
        else
        {
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
            Instantiate(medusaProjectile, firePoint.position, firePoint.rotation);
        }

        yield return new WaitForSeconds(0.5f); 
        
        isAttacking = false;
    }
}