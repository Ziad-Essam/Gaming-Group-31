using UnityEngine;

public class PuzzleRope : MonoBehaviour
{
    public int ropeID; 
    public PuzzleManager manager;
    public SpriteRenderer sr;
    
    public Color hitColor = Color.green;
    private Color originalColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateRope();
        }
    }

    void ActivateRope()
    {
        sr.color = hitColor;
        
        if(manager != null) manager.RegisterHit(ropeID);

        Invoke("ResetColor", 0.5f);
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }
}