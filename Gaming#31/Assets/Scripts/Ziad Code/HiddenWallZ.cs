using UnityEngine;

public class HiddenWallZ : MonoBehaviour
{
    [Header("Setup")]
    public GameObject fakeWallArt;   // The sprite that looks like a wall
    public Collider2D solidBlocker;  // The invisible wall that will turn ON later

    [Header("Settings")]
    public float fadeSpeed = 2f;
    private SpriteRenderer wallSR;
    private bool isRevealed = false;

    void Start()
    {
        // 1. Setup the Blocker
        if (solidBlocker != null)
        {
            solidBlocker.enabled = false; // Start OPEN so player can enter
        }

        // 2. Setup the Art
        if (fakeWallArt != null)
        {
            wallSR = fakeWallArt.GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        // Smoothly fade out the wall when revealed
        if (isRevealed && wallSR != null)
        {
            wallSR.color = Color.Lerp(wallSR.color, new Color(1, 1, 1, 0.2f), Time.deltaTime * fadeSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isRevealed = true; // Start fading the wall
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // LOCK THE DOOR!
            // When the player passes through and exits this trigger, we turn on the solid wall.
            if (solidBlocker != null)
            {
                solidBlocker.enabled = true; 
            }
        }
    }
}