using UnityEngine;

public class WitchControllerJ : EnemyControllerJ
{
    [Header("Attack Settings")]
    public GameObject fireballPrefab;    
    public Transform shootingPoint;      
    public float attackRange = 7f;       
    public float attackCooldown = 3f;    
    public float timeToDestroy = 1.5f;   

    private Transform player;
    private float nextAttackTime;

    public override void Start()
    {
        base.Start();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        nextAttackTime = Time.time;
    }

    void FixedUpdate()
    {
        if (rb != null && !isDead)
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (isDead || player == null) return;

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= attackRange)
        {
            LookAtPlayer(); 
            
            if (Time.time >= nextAttackTime)
            {
                ShootFireball();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void LookAtPlayer()
    {
        if (transform.position.x < player.position.x)
        {
            sr.flipX = true; 
        }
        else
        {
            sr.flipX = false; 
        }
    }

    void ShootFireball()
    {
        if (anim != null) anim.SetTrigger("Shoot"); 

        Vector3 directionToPlayer = (player.position - shootingPoint.position).normalized; 

        GameObject fireball = Instantiate(fireballPrefab, shootingPoint.position, Quaternion.identity);
        
        Rigidbody2D rbFireball = fireball.GetComponent<Rigidbody2D>();
        EnemyFireball fballScript = fireball.GetComponent<EnemyFireball>();
        
        if (rbFireball != null && fballScript != null)
        {
            rbFireball.linearVelocity = directionToPlayer * fballScript.speed; 
            
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            fireball.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
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
        
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        this.enabled = false; 

        if (anim != null)
        {
            anim.SetTrigger("Death");
        }

        Destroy(gameObject, timeToDestroy);
    }
}