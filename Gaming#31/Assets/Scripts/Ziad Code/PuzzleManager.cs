using UnityEngine;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public List<int> correctOrder; 
    public GameObject hiddenFloor; 

    private List<int> currentInput = new List<int>(); 

    void Start()
    {
        if(hiddenFloor != null)
            hiddenFloor.SetActive(false);
    }

    public void RegisterHit(int id)
    {
        Debug.Log("Player hit Rope " + id);
        currentInput.Add(id);

        CheckPuzzle();
    }

    void CheckPuzzle()
    {
        if (currentInput.Count > correctOrder.Count)
        {
             Debug.Log("Too many hits! Resetting...");
             currentInput.Clear();
             return;
        }

        for (int i = 0; i < currentInput.Count; i++)
        {
            if (currentInput[i] != correctOrder[i])
            {
                Debug.Log("Wrong Order! Resetting...");
                currentInput.Clear(); 
                return;
            }
        }

        if (currentInput.Count == correctOrder.Count)
        {
            Debug.Log("PUZZLE SOLVED!");
            if(hiddenFloor != null)
                hiddenFloor.SetActive(true);
            
            this.enabled = false; 
        }
    }
}