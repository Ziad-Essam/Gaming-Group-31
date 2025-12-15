using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject CurrentCheckpoint;

    public Transform player;


    void Start()
    {
        CurrentCheckpoint = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        FindObjectOfType<PlayerController>().transform.position = CurrentCheckpoint.transform.position;
 // 1. Find all active enemies in the scene
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    // 2. Loop through them and destroy them one by one
    foreach (GameObject enemy in enemies)
    {
        FindObjectOfType<PlayerController>().transform.position = CurrentCheckpoint.transform.position;
        Destroy(enemy);
    }

    // ... Your existing respawn code (moving player to checkpoint, etc.) ...
    Debug.Log("Player Respawned - Enemies Cleared");

    }
}
