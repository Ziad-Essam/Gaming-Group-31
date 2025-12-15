using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public PuzzlePlatform[] platforms; // ARRAY of platforms
    private bool activated = false;

    private Animator anim; // 🔹 ADDED

    void Start()
    {
        anim = GetComponent<Animator>(); // 🔹 ADDED
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            // 🔹 PLAY PRESS ANIMATION
            anim.SetBool("Pressed", true);

            // show all platforms
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].Show();
            }

            activated = true; // 🔒 only once
        }
    }
}
