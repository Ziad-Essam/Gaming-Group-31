using UnityEngine;

public class Firewarm : EnemyControllerYB
{
    public AppearPlatform platformToAppear;

    void FixedUpdate()
    {
        if (isDead) return;

        float dir = sr.flipX ? -1f : 1f;
        rb.linearVelocity = new Vector2(dir * maxSpeed, rb.linearVelocity.y);
    }

    public override void Die()
    {
        isDead = true;

        if (platformToAppear != null)
            platformToAppear.Appear();

        gameObject.SetActive(false); // disappear immediately
    }
}
