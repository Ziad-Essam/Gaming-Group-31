using UnityEngine;

public class OneWayWall : MonoBehaviour
{
    public GameObject visualWall;

    void Start()
    {
        // Turn OFF the whole wall at start
        visualWall.SetActive(false);
    }

    public void ActivateWall()
    {
        // Turn ON the whole wall
        visualWall.SetActive(true);
    }
}
