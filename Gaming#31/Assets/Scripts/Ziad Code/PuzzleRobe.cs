using UnityEngine;

public class PuzzleRope : MonoBehaviour
{
    public int ropeID; // 1, 2, 3...
    public PuzzleManager manager;
    public SpriteRenderer sr;
    
    public Color hitColor = Color.green;
    private Color originalColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // This function runs automatically when something enters the Trigger
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object touching us is the PLAYER
        if (collision.CompareTag("Player"))
        {
            ActivateRope();
        }
    }

    void ActivateRope()
    {
        // Visual feedback
        sr.color = hitColor;
        
        // Tell manager
        if(manager != null) manager.RegisterHit(ropeID);

        // Reset color after 0.5 seconds
        Invoke("ResetColor", 0.5f);
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }
}