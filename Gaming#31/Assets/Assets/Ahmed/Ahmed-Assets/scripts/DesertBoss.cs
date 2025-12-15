using UnityEngine;

public class DesertBoss : EnemyController
{
    [Header("Detection")]
    public float detectDistance = 6f;

    [Header("Shooting")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float shootCooldown = 2f;
    public float fireballSpeed = 5f;

    private Transform player;
    private float shootTimer = 0f;

    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Boss should NEVER move
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    void Update()
    {
        if (player == null)
            return;

        shootTimer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.position);

        // Player too far do nothing
        if (distance > detectDistance)
            return;

        // Face the player
        sr.flipX = player.position.x < transform.position.x;

        // Shoot on cooldown
        if (shootTimer >= shootCooldown)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("Attack");

        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject fireball = Instantiate(
            fireballPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Rigidbody2D rbFire = fireball.GetComponent<Rigidbody2D>();
        rbFire.linearVelocity = direction * fireballSpeed;
    }
}
