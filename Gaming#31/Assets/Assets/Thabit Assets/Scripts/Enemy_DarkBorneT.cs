using System.Collections;
using UnityEngine;

public class Enemy_DarkBorneT : EnemyControllerT
{
    [Header("Manual Weapon Setup")]
    public GameObject EnemyWeapon; // Drag your EnemyWeapon here

    [Header("AI Settings")]
    public float chaseRange = 5f;
    public float attackRange = 1.2f;
    public float attackCooldown = 2f;

    private Transform player;
    private float nextAttackTime = 0f;
    private Animator anim;
    private Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        // Ensure weapon is off at start
        if (EnemyWeapon != null) EnemyWeapon.SetActive(false);
    }

    void FixedUpdate()
    {
    if (isDead) return;  // Prevent all movement/attack logic


        if (player == null) return;

        // Horizontal distance only
        float distToPlayer = Mathf.Abs(player.position.x - transform.position.x);

        if (distToPlayer < attackRange)
{
    rb.linearVelocity = Vector2.zero;

    if (Time.time >= nextAttackTime)
    {
        anim.SetBool("isMoving", false);
        StartCoroutine(PerformAttack());
        nextAttackTime = Time.time + attackCooldown;
    }
    else
    {
        anim.SetBool("isMoving", false);
    }
}
        else if (distToPlayer < chaseRange)
        {
            // --- CHASE STATE ---
            ChasePlayer();
            anim.SetBool("isMoving", true); // play run
        }
        else
        {
            // --- IDLE / PATROL ---
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }

    void ChasePlayer()
    {
        float direction = player.position.x - transform.position.x;
        rb.linearVelocity = new Vector2(Mathf.Sign(direction) * maxSpeed, rb.linearVelocity.y);

        // Flip sprite
        sr.flipX = direction < 0;
    }

   IEnumerator PerformAttack()
{
    rb.linearVelocity = Vector2.zero;
    anim.SetBool("isMoving", false);
    anim.SetTrigger("attack");

    yield return new WaitForSeconds(0.2f);
    if (EnemyWeapon != null)
    {
        EnemyWeapon.SetActive(true);
    }

    yield return new WaitForSeconds(0.3f);
    if (EnemyWeapon != null)
    {
        EnemyWeapon.SetActive(false);
    }
}
    
[Header("Platform Unlock")]
public GameObject[] platformsToUnlock; // Drag your platforms here in Inspector

public override void Die()
{
    if (isDead) return;
    isDead = true;

    // Stop all movement
    rb.linearVelocity = Vector2.zero;
    rb.bodyType = RigidbodyType2D.Static;
    
    // DON'T set isMoving to false - it might be interrupting
    // anim.SetBool("isMoving", false); // REMOVE THIS LINE
    
    anim.SetTrigger("IsDead");

    // Unlock platforms
    foreach (GameObject platform in platformsToUnlock)
    {
        if (platform != null)
            platform.SetActive(true);
    }

    StartCoroutine(DeathRoutine());
    FindObjectOfType<BossRoomTrigger>().UnlockRoom();
}

IEnumerator DeathRoutine()
{
    for (int i = 0; i < 10; i++)
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        Debug.Log($"Frame {i}: normalizedTime = {state.normalizedTime}");
        yield return new WaitForSeconds(0.1f);
    }
    
    yield return new WaitForSeconds(deathAnimDuration);
    Destroy(gameObject);
}
}
