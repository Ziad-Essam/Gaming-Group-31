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

    protected override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    void Update()
    {
        if (player == null || isDead)
            return;

        shootTimer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > detectDistance)
            return;

        sr.flipX = player.position.x < transform.position.x;

        if (shootTimer >= shootCooldown)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
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
    public override void Die()
    {
        if (isDead) return;
        isDead = true;

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        anim.SetTrigger("IsDead");

        StartCoroutine(DeathRoutine());
    }
    private System.Collections.IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.0f); 
        Destroy(gameObject);
    }
}
