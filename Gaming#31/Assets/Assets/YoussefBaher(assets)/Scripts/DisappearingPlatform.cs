using System.Collections;
using UnityEngine;

public class DisappearPlatform : MonoBehaviour
{
    public float time = 1.5f;
    public float backTime = 3f;

    Collider2D box;
    SpriteRenderer sprite;
    Rigidbody2D rb;

    void Start()
    {
        box = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(waitAndFall());
        }
    }

    IEnumerator waitAndFall()
    {
        yield return new WaitForSeconds(time);

        box.enabled = false;
        sprite.enabled = false;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 2;

        yield return new WaitForSeconds(backTime);

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;

        box.enabled = true;
        sprite.enabled = true;
    }
}
