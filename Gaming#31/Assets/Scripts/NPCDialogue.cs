using UnityEngine;

public class NPCDialogue2 : MonoBehaviour
{
    public Dialogue dialogueManager;

    [TextArea(2, 5)]
    public string[] dialogue;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.SetSentences(dialogue);
            dialogueManager.StartDialogue();

            Destroy(GetComponent<Collider2D>(), 5f);
        }
    }
}
