using UnityEngine;

public class HiddenWallZ : MonoBehaviour
{
    [Header("Setup")]
    public GameObject fakeWallArt;   
    public Collider2D solidBlocker;  
    [Header("Settings")]
    public float fadeSpeed = 2f;
    private SpriteRenderer wallSR;
    private bool isRevealed = false;

    void Start()
    {
        if (solidBlocker != null)
        {
            solidBlocker.enabled = false; 
        }

        if (fakeWallArt != null)
        {
            wallSR = fakeWallArt.GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (isRevealed && wallSR != null)
        {
            wallSR.color = Color.Lerp(wallSR.color, new Color(1, 1, 1, 0.2f), Time.deltaTime * fadeSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isRevealed = true; 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (solidBlocker != null)
            {
                solidBlocker.enabled = true; 
            }
        }
    }
}