using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform enemy;

    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
