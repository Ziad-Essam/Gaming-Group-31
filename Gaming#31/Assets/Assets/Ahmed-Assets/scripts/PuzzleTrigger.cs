using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public Rigidbody2D box;
    public RisingPlatform platform;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;
            box.bodyType = RigidbodyType2D.Dynamic;
            platform.Activate();
        }
    }
}
