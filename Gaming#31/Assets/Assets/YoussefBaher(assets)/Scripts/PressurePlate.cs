using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public PuzzlePlatform[] platforms;
    private bool activated = false;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            
            anim.SetBool("Pressed", true);

            
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].Show();
            }

            activated = true;
        }
    }
}
