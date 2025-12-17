using UnityEngine;

public class MovingPlatformJ : MonoBehaviour
{
    public float moveSpeed = 1.5f;             
    public float moveDistance = 4f;            

    
    private Vector3 startPos;                  
    private float oscillationTimer = 0f;      
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        oscillationTimer += Time.deltaTime * moveSpeed;

        float yMovement = Mathf.Sin(oscillationTimer) * moveDistance;

        Vector3 newPos = new Vector3(startPos.x, startPos.y + yMovement, startPos.z);
        
        transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}