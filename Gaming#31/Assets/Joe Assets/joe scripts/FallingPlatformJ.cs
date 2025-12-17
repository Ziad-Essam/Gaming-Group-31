using UnityEngine;
using System.Collections;

public class FallingPlatformJ : MonoBehaviour
{
    public float fallDelay = 1.5f;
    public float returnDelay = 6f;

    private Rigidbody2D rb;
    private Vector3 startPos;
    private bool triggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !triggered)
        {
            triggered = true;
            StartCoroutine(FallAndReturn());
        }
    }

    IEnumerator FallAndReturn()
    {
        yield return new WaitForSeconds(fallDelay);

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 2.5f;

        yield return new WaitForSeconds(returnDelay);

        // reset
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        transform.position = startPos;

        triggered = false;
    }
}