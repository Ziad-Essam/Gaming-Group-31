using UnityEngine;

public class WitchController : EnemyController
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
        // Enforce Stationary Position
        if (rb != null && !isDead)
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (isDead || player == null) return;

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= attackRange)
        {
            // 1. Witch faces the player visually 
            LookAtPlayer(); 
            
            // 2. Check Cooldown
            if (Time.time >= nextAttackTime)
            {
                ShootFireball();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    // Witch flips to face the player for visual coherence
    void LookAtPlayer()
    {
        if (transform.position.x < player.position.x)
        {
            sr.flipX = true; // Player is right: Witch faces right
        }
        else
        {
            sr.flipX = false; // Player is left: Witch faces left
        }
    }

    void ShootFireball()
    {
        if (anim != null) anim.SetTrigger("Shoot"); 

        // 1. Calculate the vector from the shooting point TOWARDS the player
        Vector3 directionToPlayer = (player.position - shootingPoint.position).normalized; 

        // 2. Create the fireball instance
        GameObject fireball = Instantiate(fireballPrefab, shootingPoint.position, Quaternion.identity);
        
        // 3. Get components 
        Rigidbody2D rbFireball = fireball.GetComponent<Rigidbody2D>();
        EnemyFireball fballScript = fireball.GetComponent<EnemyFireball>();
        
        // 4. Set linearVelocity 
        if (rbFireball != null && fballScript != null)
        {
            rbFireball.linearVelocity = directionToPlayer * fballScript.speed; 
            
            // 5. Rotate the projectile sprite to face the direction of travel
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            fireball.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
    
    // OVERRIDE: Takes damage, calls base logic, and plays Hurt animation.
    public override void TakeDamage(int damageAmount)
    {
        if (isDead) return;
        
        base.TakeDamage(damageAmount); 

        if (!isDead && anim != null) 
        {
            anim.SetTrigger("Hurt"); 
        }
    }
    
    // OVERRIDE: Handles death specific to the Witch (animation and delayed destroy).
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