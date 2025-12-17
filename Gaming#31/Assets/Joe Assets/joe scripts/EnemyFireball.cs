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
        if ((timer += Time.deltaTime) >= lifeTime)
        {
            Destroy(gameObject); 
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
            
            Destroy(gameObject); 
        }
        else if (other.CompareTag("Wall")) 
        {
            Destroy(gameObject); 
        }
    }
}