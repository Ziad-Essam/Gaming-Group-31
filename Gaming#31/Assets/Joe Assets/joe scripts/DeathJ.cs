using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathJ : MonoBehaviour
{

   private void OnTriggerEnter2D(Collider2D other)
    {

        PlayerStats player = other.GetComponent<PlayerStats>();
        if (other.tag == "Player")
        {
            player.TakeDamage(PlayerStats.maxHealth);
            
        }
    }
    void Start()
    {

    }

    void Update()
    {
        

    }
}