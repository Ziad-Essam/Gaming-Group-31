using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathJ : MonoBehaviour
{
    // Start is called before the first frame update

    private  void OnTriggerEnter2D(Collider2D other)
    {
         if (other.tag=="Player")
         FindObjectOfType<LevelManager>().RespawnPlayer();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}