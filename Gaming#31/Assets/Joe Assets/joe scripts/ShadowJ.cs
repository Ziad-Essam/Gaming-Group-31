using System.Collections;
using UnityEngine;

public class ShadowJ : EnemyControllerJ
{
    [Header("Weapon")]
    public GameObject shadowWeapon;

    [Header("AI Settings")]
    public float chaseRange = 5f;
    public float attackRange = 1.2f;
    public float attackCooldown = 2f;

    private Transform player;
    private float nextAttackTime = 0f;

    public override void Start()
    {
        base.Start(); 

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        if (shadowWeapon != null)
            shadowWeapon.SetActive(false);
    }

    void FixedUpdate()
    {
        if (isDead || player == null) return; 

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;

            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(PerformAttack());
                nextAttackTime = Time.time + attackCooldown;
            }
            else
            {
                anim.SetBool("IsRunning", false);
            }
        }
        else if (distToPlayer <= chaseRange)
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
            transform.rotation = Quaternion.Euler(0, 180, 0); // Face right
        }
        else
        {
            rb.linearVelocity = new Vector2(-maxSpeed, rb.linearVelocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0); // Face left
        }
    }

    IEnumerator PerformAttack()
    {
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.2f);

        if (shadowWeapon != null)
            shadowWeapon.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        if (shadowWeapon != null)
            shadowWeapon.SetActive(false);
    }

    public override void TakeDamage(int damageAmount)
    {
        if (isDead) return;
        base.TakeDamage(damageAmount);
        
        if (!isDead && anim != null) 
        {
            anim.SetTrigger("Hurt"); 
        }
    }

    public override void Die()
    {
        if (isDead) return; 
        isDead = true;

        if (anim != null) anim.SetTrigger("death"); 

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        if (shadowWeapon != null)
            shadowWeapon.SetActive(false);

        this.enabled = false;

        Destroy(gameObject, 1.5f);
    }
}