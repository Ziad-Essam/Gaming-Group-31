using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float Cameraspeed = 5f;

    public float yOffset = 2f;      
    public float deadZoneHeight = 1.5f; 

    public float minX, maxX;
    public float minY, maxY;

    private Vector3 lastTargetPosition;

    void Start()
    {
        if(Target != null) 
        {
            lastTargetPosition = Target.position;
        }
    }

    void LateUpdate()
    {
        if (Target != null)
        {
            float distanceMoved = Vector3.Distance(Target.position, lastTargetPosition);
            
            if(distanceMoved > 5f)
            {
                SnapToCenter();
            }
            else
            {
                float idealY = Target.position.y + yOffset;
                float currentY = transform.position.y;

                float diffY = idealY - currentY;
                float targetY = currentY; 

                if (diffY > deadZoneHeight)
                {
                    targetY = idealY - deadZoneHeight;
                }
                else if (diffY < -deadZoneHeight)
                {
                    targetY = idealY + deadZoneHeight;
                }

                float newY = Mathf.Lerp(currentY, targetY, Time.deltaTime * Cameraspeed);
                float newX = Mathf.Lerp(transform.position.x, Target.position.x, Time.deltaTime * Cameraspeed);

                newX = Mathf.Clamp(newX, minX, maxX);
                newY = Mathf.Clamp(newY, minY, maxY);

                transform.position = new Vector3(newX, newY, -10f);
            }

            lastTargetPosition = Target.position;
        }
    }   

    void SnapToCenter()
    {
        float finalX = Mathf.Clamp(Target.position.x, minX, maxX);
        float finalY = Mathf.Clamp(Target.position.y + yOffset, minY, maxY);
        transform.position = new Vector3(finalX, finalY, -10f);
    }
}