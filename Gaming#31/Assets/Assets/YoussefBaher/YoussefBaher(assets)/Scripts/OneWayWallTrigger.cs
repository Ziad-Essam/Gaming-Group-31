using UnityEngine;

public class OneWayWallTrigger : MonoBehaviour
{
    public OneWayWall wall;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER HIT TRIGGER");
            wall.ActivateWall();
        }
    }
}
