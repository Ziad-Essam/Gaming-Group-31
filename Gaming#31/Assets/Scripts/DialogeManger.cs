using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI textDisplay;
    public GameObject continueButton;
    public GameObject dialogueBox;

    [Header("Player")]
    public Rigidbody2D playerRB;

    [Header("Settings")]
    public float typingSpeed = 0.03f;

    // Private
    private string[] dialogueSentences;
    private int index = 0;
    private Coroutine typingCoroutine;

    void Start()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
    }

    // Called from NPC
    public void SetSentences(string[] sentences)
    {
        dialogueSentences = sentences;
        index = 0;
        textDisplay.text = "";
    }

    // Start dialogue safely
    public void StartDialogue()
    {
        if (dialogueSentences == null || dialogueSentences.Length == 0)
            return;

        dialogueBox.SetActive(true);
        continueButton.SetActive(false);

        // Freeze player
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX |
                               RigidbodyConstraints2D.FreezePositionY |
                               RigidbodyConstraints2D.FreezeRotation;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeDialogue());
    }

    public void NextSentence()
    {
        // 🔒 SAFETY CHECK (prevents your error)
        if (dialogueSentences == null || dialogueSentences.Length == 0)
            return;

        continueButton.SetActive(false);

        if (index < dialogueSentences.Length - 1)
        {
            index++;
            textDisplay.text = "";

            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeDialogue());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeDialogue()
    {
        foreach (char letter in dialogueSentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
        textDisplay.text = "";

        dialogueSentences = null;
        index = 0;

        // Unfreeze player (movement allowed again)
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
