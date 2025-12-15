using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float speed = 5f;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = PointB.position;
    }

    void Update()
    {
        // Move the platform
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        // Check if we reached the target, then swap
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            targetPosition = targetPosition == PointA.position
                ? PointB.position
                : PointA.position;
        }
    }

    // --- COLLISION LOGIC ---

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 1. YOUR FIX: Only parent if the player is ABOVE the platform.
            // We add '0.5f' (half a block height) to be safe. 
            // This ensures we don't grab the player if they hit the side corners.
            if (collision.transform.position.y > transform.position.y + 0.3f)
            {
                collision.transform.SetParent(transform);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Safety Check: If platform is disabled, don't try to unparent
        if (!gameObject.activeInHierarchy) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            // Always remove parent when leaving, regardless of direction
            collision.transform.SetParent(null);
        }
    }
}