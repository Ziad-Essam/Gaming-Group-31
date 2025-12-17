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
        
        rb.gravityScale = 0; 
        rb.freezeRotation = true; 
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;

            rb.linearVelocity = direction * speed;
            
        }
        else
        {
            rb.linearVelocity = transform.right * speed;
        }
        
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