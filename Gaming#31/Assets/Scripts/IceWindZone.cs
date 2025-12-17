using UnityEngine;
using System.Collections;

public class IceWindZone : MonoBehaviour
{
    public float windForce = 8f;
    public float windOnTime = 2f;
    public float windOffTime = 1.5f;

    private bool windActive = true;

    public ParticleSystem windParticles;

void Start()
{
    windParticles.Play();
    StartCoroutine(WindCycle());
}

    IEnumerator WindCycle()
    {
    

    while (true)
    {
        windActive = true;
        windParticles.Play();
        yield return new WaitForSeconds(windOnTime);

        windActive = false;
        windParticles.Stop();
        yield return new WaitForSeconds(windOffTime);
    }


        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!windActive) return;

        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.left * windForce, ForceMode2D.Force);
        }
    }
}
