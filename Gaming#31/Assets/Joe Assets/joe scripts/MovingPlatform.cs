using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // --- Configurable Variables (Set in Inspector) ---
    public float moveSpeed = 1.5f;             // How fast the platform moves
    public float moveDistance = 4f;            // How far up and down it moves from its starting point

    // --- Private Variables ---
    private Vector3 startPos;                  // The position where the platform starts
    private float oscillationTimer = 0f;       // Timer to control the movement cycle

    void Start()
    {
        // Save the initial position of the platform
        startPos = transform.position;
    }

    void Update()
    {
        // Increase the timer based on real-time
        // The speed is multiplied by Time.deltaTime to make it frame-rate independent
        oscillationTimer += Time.deltaTime * moveSpeed;

        // Use Math.Sin to create smooth, back-and-forth movement
        // Math.Sin outputs a value between -1 and 1
        float yMovement = Mathf.Sin(oscillationTimer) * moveDistance;

        // Calculate the new position
        // Only the Y-coordinate changes (vertical movement)
        Vector3 newPos = new Vector3(startPos.x, startPos.y + yMovement, startPos.z);
        
        // Apply the new position to the platform
        transform.position = newPos;
    }

    // --- FIX FOR PLAYER MOVEMENT ---
    // This function ensures the player moves with the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set the platform as the parent of the player's Transform.
            // This makes the player move with the platform's Transform.
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the collided object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // When the player leaves the platform, remove the parent relationship.
            // Setting the parent to null makes the player move independently again.
            collision.collider.transform.SetParent(null);
        }
    }
}