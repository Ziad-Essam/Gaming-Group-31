using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    public float speed = 5f;     
    public int damage = 10;      

    [SerializeField] private float lifeTime = 2f; 

    private Rigidbody2D rb;
    private float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Destroy after a timeout to prevent infinite travel
        if ((timer += Time.deltaTime) >= lifeTime)
        {
            Destroy(gameObject); 
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Check if the fireball hits the Player
        if (other.CompareTag("Player")) 
        {
            // Try to get the PlayerStats script 
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
            
            Destroy(gameObject); // Destroy the fireball on hit
        }
        // 2. Check if the fireball hits a Wall/Obstacle
        else if (other.CompareTag("Wall")) 
        {
            Destroy(gameObject); // Destroy the fireball on wall hit
        }
    }
}