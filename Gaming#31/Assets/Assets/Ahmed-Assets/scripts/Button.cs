using UnityEngine;

public class Button : MonoBehaviour
{
    public Gate gate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            gate.Open();
        }
    }
}