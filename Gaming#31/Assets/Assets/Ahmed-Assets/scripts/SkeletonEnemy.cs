using System.Collections;
using UnityEngine;

public class SkeletonEnemy : EnemyController
{
    public GameObject goblinWeapon;
    public float chaseRange = 5f;
    public float attackRange = 1.2f;
    public float attackCooldown = 2f;

    private Transform player;
    private bool isAttacking = false;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (goblinWeapon != null)
            goblinWeapon.SetActive(false);
    }

    void Update()
    {
        if (player == null || isDead) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("IsRunning", false);

            if (!isAttacking)
                StartCoroutine(AttackOnce());
        }
        else if (dist <= chaseRange)
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
        float dir = Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(dir * maxSpeed, rb.linearVelocity.y);
        transform.rotation = dir > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
    }

    IEnumerator AttackOnce()
    {
        isAttacking = true;

        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.25f);
        goblinWeapon.SetActive(true);

        yield return new WaitForSeconds(0.25f);
        goblinWeapon.SetActive(false);

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
