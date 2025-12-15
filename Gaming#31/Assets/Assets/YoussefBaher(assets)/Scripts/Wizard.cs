using UnityEngine;

public class Wizard : EnemyController
{
    [Header("Movement")]
    public float speed = 2f;
    public float detectDistance = 6f;
    public float stopDistance = 2f;

    [Header("Shooting")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float shootCooldown = 2f;

    private Transform player;
    private float shootTimer = 0f;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (dead || player == null)
            return;

        shootTimer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.position);

        // TOO FAR → IDLE
        if (distance > detectDistance)
        {
            anim.SetBool("IsWalking", false);
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // WALK
        if (distance > stopDistance)
        {
            anim.SetBool("IsWalking", true);

            Vector2 dir = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(dir.x * speed, rb.linearVelocity.y);

            sr.flipX = dir.x < 0;
        }
        else
        {
            // STOP AND SHOOT
            anim.SetBool("IsWalking", false);
            rb.linearVelocity = Vector2.zero;

            if (shootTimer >= shootCooldown)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        if (fireballPrefab == null || firePoint == null)
        {
            Debug.LogError("Wizard missing fireballPrefab or firePoint!");
            return;
        }

        anim.SetTrigger("Attack");

        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject fireball = Instantiate(
            fireballPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Rigidbody2D rbFire = fireball.GetComponent<Rigidbody2D>();
        if (rbFire != null)
        {
            rbFire.linearVelocity = direction * 5f;
        }
        else
        {
            Debug.LogError("Fireball has NO Rigidbody2D!");
        }
    }
}
