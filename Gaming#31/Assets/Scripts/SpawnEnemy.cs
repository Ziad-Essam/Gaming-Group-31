using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform enemy;

    public Transform spawnPoint;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void RespawnEnemey()
    {
        Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
    
     void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
           RespawnEnemey();
        }
    }
}
