using System.Collections;
using UnityEngine;

public class TimedPlatform : MonoBehaviour
{
    public float onTime = 2f;
    public float offTime = 2f;

    SpriteRenderer sprite;
    Collider2D box;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<Collider2D>();

        StartCoroutine(loop());
    }

    IEnumerator loop()
    {
        while (true)
        {
            // platform ON
            sprite.enabled = true;
            box.enabled = true;

            yield return new WaitForSeconds(onTime);

            // platform OFF
            sprite.enabled = false;
            box.enabled = false;

            yield return new WaitForSeconds(offTime);
        }
    }
}
