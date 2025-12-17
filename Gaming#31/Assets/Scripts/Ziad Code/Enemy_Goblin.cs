using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Goblin : EnemyControllerZ
{
    [Header("Manual Weapon Setup")]
    public GameObject goblinWeapon; 
    [Header("AI Settings")]
    public float chaseRange = 5f;
    public float attackRange = 1.2f;
    public float attackCooldown = 2f;

    private Transform player;
    private float nextAttackTime = 0;
    private Animator anim;
    private Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
        
        
        if(goblinWeapon != null) goblinWeapon.SetActive(false);
    }

    void FixedUpdate()
    {
        
        if (player == null) return;

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < attackRange)
        {
           
            rb.linearVelocity = Vector2.zero; 
            
          
            if (Time.time < nextAttackTime)
            {
                anim.SetBool("IsRunning", false); 
            }
            
            else
            {
                StartCoroutine(PerformAttack());
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else if (distToPlayer < chaseRange)
        {
            ChasePlayer();
            anim.SetBool("IsRunning", true); 
        }
        else
        {
            rb.linearVelocity = Vector2.zero; 
            anim.SetBool("IsRunning", false); 
        }
    }

   void ChasePlayer()
    {
        
        if (transform.position.x < player.position.x)
        {
            rb.linearVelocity = new Vector2(maxSpeed, rb.linearVelocity.y);
            
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-maxSpeed, rb.linearVelocity.y);
            
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    IEnumerator PerformAttack()
    {
        anim.SetTrigger("Attack"); 
        yield return new WaitForSeconds(0.2f); 
        
        if(goblinWeapon != null) goblinWeapon.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        if(goblinWeapon != null) goblinWeapon.SetActive(false);
    }
}





