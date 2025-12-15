using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy: EnemyController
{
    [Header("Manual Weapon Setup")]
    public GameObject goblinWeapon; // DRAG YOUR EMPTY OBJECT HERE

    [Header("AI Settings")]
    public float chaseRange = 5f;
    public float attackRange = 1.2f;
    public float attackCooldown = 2f;

    private Transform player;
    private float nextAttackTime = 0;
    private Animator anim;
  

    public override void Start()
    {
        base.Start();
       
        anim = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        // Ensure weapon is off at start
        if (goblinWeapon != null) goblinWeapon.SetActive(false);
    }

    void FixedUpdate()
    {

        if (player == null) return;

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < attackRange)
        {
            // --- STATE: ATTACKING / WAITING ---
            rb.linearVelocity = Vector2.zero; // Stop moving

            // If we are waiting for cooldown, play IDLE
            if (Time.time < nextAttackTime)
            {
                anim.SetBool("IsRunning", false); // Go to Idle
            }
            // If cooldown is finished, ATTACK
            else
            {
                StartCoroutine(PerformAttack());
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else if (distToPlayer < chaseRange)
        {
            // --- STATE: CHASING ---
            ChasePlayer();
            anim.SetBool("IsRunning", true); // Play Run anim
        }
        else
        {
            // --- STATE: IDLE / PATROL ---
            rb.linearVelocity = Vector2.zero; // Stop for now (or add patrol logic)
            anim.SetBool("IsRunning", false); // Play Idle
        }
    }

    void ChasePlayer()
    {
        // 1. Determine which side the player is on
        if (transform.position.x < player.position.x)
        {
            // --- PLAYER IS TO THE RIGHT ---
            rb.linearVelocity = new Vector2(maxSpeed, rb.linearVelocity.y);

            // Rotate to face RIGHT (180 degrees)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            // --- PLAYER IS TO THE LEFT ---
            rb.linearVelocity = new Vector2(-maxSpeed, rb.linearVelocity.y);

            // Rotate to face LEFT (0 degrees - Default)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    // This Coroutine handles the attack timing smoothly
    IEnumerator PerformAttack()
    {
        anim.SetTrigger("Attack"); // Play animation

        // Wait a tiny bit so the sword actually swings visually before damage happens
        yield return new WaitForSeconds(0.2f);

        // Enable the HITBOX (The object you made)
        if (goblinWeapon != null) goblinWeapon.SetActive(true);

        // Keep it on for a split second
        yield return new WaitForSeconds(0.3f);

        // Disable the HITBOX
        if (goblinWeapon != null) goblinWeapon.SetActive(false);
    }
}