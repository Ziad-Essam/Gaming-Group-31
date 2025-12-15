using UnityEngine;

public class Firewarm : EnemyController
{
    public AppearPlatform platformToAppear;

    void FixedUpdate()
    {
        if (dead) return;

        float dir = sr.flipX ? -1f : 1f;
        rb.linearVelocity = new Vector2(dir * maxSpeed, rb.linearVelocity.y);
    }

    protected override void Die()
    {
        dead = true;

        if (platformToAppear != null)
            platformToAppear.Appear();

        gameObject.SetActive(false); // disappear immediately
    }
}
