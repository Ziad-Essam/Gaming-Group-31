using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadly : MonoBehaviour
{

    public int damageAmount = 100; // Amount to kill instantly (assuming Max HP is 100)

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"){
            
          FindObjectOfType<PlayerStats>().TakeDamage(damageAmount);
        FindObjectOfType<LevelManager>().RespawnPlayer();
    }
    }
}
