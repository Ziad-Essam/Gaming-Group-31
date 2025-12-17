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

    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        FindObjectOfType<PlayerController>().transform.position = CurrentCheckpoint.transform.position;
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");

    foreach (GameObject enemy in enemies)
    {
        FindObjectOfType<PlayerController>().transform.position = CurrentCheckpoint.transform.position;
        Destroy(enemy);
    }

    Debug.Log("Player Respawned - Enemies Cleared");

    }
}
