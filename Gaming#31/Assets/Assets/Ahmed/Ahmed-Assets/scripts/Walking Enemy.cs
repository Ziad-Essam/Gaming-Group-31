using UnityEngine;

public class WalkingEnemy : EnemyController
{
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.flipX = false;
    }

    void FixedUpdate()
    {
        if (sr.flipX)
        {
            GetComponent<Rigidbody2D>().linearVelocity =
                new Vector2(-maxSpeed, GetComponent<Rigidbody2D>().linearVelocity.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().linearVelocity =
                new Vector2(maxSpeed, GetComponent<Rigidbody2D>().linearVelocity.y);
        }
    }
}
