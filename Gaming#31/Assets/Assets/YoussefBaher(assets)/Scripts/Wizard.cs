using UnityEngine;

public class Wizard : EnemyControllerYB
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
        if (isDead || player == null)
            return;

        shootTimer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > detectDistance)
        {
            anim.SetBool("IsWalking", false);
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (distance > stopDistance)
        {
            anim.SetBool("IsWalking", true);

            float dir = player.position.x > transform.position.x ? 1f : -1f;
            rb.linearVelocity = new Vector2(dir * speed, 0f);
            sr.flipX = dir < 0;
        }
        else
        {
            anim.SetBool("IsWalking", false);
            rb.linearVelocity = Vector2.zero;

            if (shootTimer >= shootCooldown)
            {
                anim.SetTrigger("Attack");
                Shoot(); 
                shootTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        if (fireballPrefab == null || firePoint == null)
            return;

        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject fireball = Instantiate(
            fireballPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Rigidbody2D rbFire = fireball.GetComponent<Rigidbody2D>();
        rbFire.linearVelocity = direction * 5f;
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
    yield return new WaitForSeconds(1.0f); // match death animation length
    Destroy(gameObject);
}


}

