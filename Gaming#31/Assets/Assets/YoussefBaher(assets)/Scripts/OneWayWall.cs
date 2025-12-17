using UnityEngine;

public class OneWayWall : MonoBehaviour
{
    public GameObject visualWall;

    void Start()
    {
        visualWall.SetActive(false);
    }

    public void ActivateWall()
    {
        visualWall.SetActive(true);
    }
}
