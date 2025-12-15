using UnityEngine;

public class Wizard : EnemyController
{
    public float speed = 2f;
    public float detectDistance = 6f;
    public float stopDistance = 2f;

    public GameObject fireballPrefab;
    public Transform firePoint;
    public float shootCooldown = 2f;

    Transform player;
    float shootTimer;

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

            float dir = player.position.x > transform.position.x ? 1 : -1;
            rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);

            sr.flipX = dir < 0;
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
        anim.SetTrigger("Attacking");

        float dir = sr.flipX ? -1f : 1f;

        GameObject fireball = Instantiate(
            fireballPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Rigidbody2D rbFire = fireball.GetComponent<Rigidbody2D>();
        rbFire.linearVelocity = new Vector2(dir * 5f, 0f);
    }
}
