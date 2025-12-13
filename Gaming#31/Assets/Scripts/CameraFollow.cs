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

    // We use this to detect if the player teleported
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
            // 1. AUTO-DETECT TELEPORT (Death/Respawn)
            // If player moved more than 5 units in one frame, it's a teleport!
            float distanceMoved = Vector3.Distance(Target.position, lastTargetPosition);
            
            if(distanceMoved > 5f)
            {
                // SNAP INSTANTLY to the perfect center (Ignore smoothing)
                SnapToCenter();
            }
            else
            {
                // 2. NORMAL MOVEMENT (Box Logic)
                float idealY = Target.position.y + yOffset;
                float currentY = transform.position.y;

                float diffY = idealY - currentY;
                float targetY = currentY; 

                // If player pushes top of box
                if (diffY > deadZoneHeight)
                {
                    targetY = idealY - deadZoneHeight;
                }
                // If player pushes bottom of box
                else if (diffY < -deadZoneHeight)
                {
                    targetY = idealY + deadZoneHeight;
                }

                // Smoothly move
                float newY = Mathf.Lerp(currentY, targetY, Time.deltaTime * Cameraspeed);
                float newX = Mathf.Lerp(transform.position.x, Target.position.x, Time.deltaTime * Cameraspeed);

                // Clamp to map boundaries
                newX = Mathf.Clamp(newX, minX, maxX);
                newY = Mathf.Clamp(newY, minY, maxY);

                transform.position = new Vector3(newX, newY, -10f);
            }

            // Update memory for next frame
            lastTargetPosition = Target.position;
        }
    }   

    void SnapToCenter()
    {
        // Instantly forces camera to correct spot + offset
        float finalX = Mathf.Clamp(Target.position.x, minX, maxX);
        float finalY = Mathf.Clamp(Target.position.y + yOffset, minY, maxY);
        transform.position = new Vector3(finalX, finalY, -10f);
    }
}