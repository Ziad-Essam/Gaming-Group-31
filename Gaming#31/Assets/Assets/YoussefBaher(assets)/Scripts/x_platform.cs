using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x_platform : MonoBehaviour
{
    public  Transform PointA;
     public  Transform PointB;
     public float speed;
     private bool AtoB;
     private Vector3 targetPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(AtoB==true){
        targetPosition=PointA.position;
        }
        else
        {
             targetPosition=PointB.position;
        }
        Vector3 newPosition=transform.position;
        newPosition.x=Mathf.MoveTowards(transform.position.x, targetPosition.x, speed*Time.deltaTime);

        transform.position=newPosition;

        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f)
        {
            AtoB=!AtoB;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}

