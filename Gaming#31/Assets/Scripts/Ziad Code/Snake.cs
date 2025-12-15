using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] 
public class MedusaProjectile : MonoBehaviour
{
    public float speed = 7f;
    public int damage = 1;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // 1. SETUP PHYSICS
        rb.gravityScale = 0; 
        rb.freezeRotation = true; 
        
        // 2. FIND TARGET
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // A. CALCULATE DIRECTION (From Code 1)
            // This gets the exact line from the Gun to the Player
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // B. SET VELOCITY (From Code 1)
            // This makes it fly TOWARDS the player (Tracking)
            rb.linearVelocity = direction * speed;
            
            // C. ROTATION (From Code 2)
            // WE DO NOTHING! 
            // We rely on the Enemy Script to have already set the Y-Rotation (Left/Right).
            // This prevents the "Upside Down" bug.
        }
        else
        {
            // Fallback if no player found: just fly forward
            rb.linearVelocity = transform.right * speed;
        }
        
        // Safety destroy after 4 seconds
        Destroy(gameObject, 4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet hit: " + other.name); 

        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats == null) stats = other.GetComponentInParent<PlayerStats>();
            if (stats == null) stats = FindObjectOfType<PlayerStats>();

            if (stats != null) stats.TakeDamage(damage);
            
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}