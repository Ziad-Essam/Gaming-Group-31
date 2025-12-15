using UnityEngine;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public List<int> correctOrder; // The secret code (e.g., 1, 3, 2)
    public GameObject hiddenFloor; // The floor to appear

    private List<int> currentInput = new List<int>(); // What the player has hit so far

    void Start()
    {
        // Hide the floor at the start
        if(hiddenFloor != null)
            hiddenFloor.SetActive(false);
    }

    public void RegisterHit(int id)
    {
        Debug.Log("Player hit Rope " + id);
        currentInput.Add(id);

        // Check if the input so far matches the correct order
        CheckPuzzle();
    }

    void CheckPuzzle()
    {
        // SAFETY CHECK 1: If player hit more ropes than the puzzle allows, reset immediately.
        if (currentInput.Count > correctOrder.Count)
        {
             Debug.Log("Too many hits! Resetting...");
             currentInput.Clear();
             return;
        }

        // 2. Check for mistakes so far
        // We only loop up to 'currentInput.Count' so we don't go out of range
        for (int i = 0; i < currentInput.Count; i++)
        {
            if (currentInput[i] != correctOrder[i])
            {
                Debug.Log("Wrong Order! Resetting...");
                currentInput.Clear(); 
                return;
            }
        }

        // 3. Check if the puzzle is FULLY complete
        if (currentInput.Count == correctOrder.Count)
        {
            Debug.Log("PUZZLE SOLVED!");
            if(hiddenFloor != null)
                hiddenFloor.SetActive(true);
            
            // Optional: Disable the manager so you can't solve it twice
            this.enabled = false; 
        }
    }
}