using UnityEngine;

public class RisingPlatform : MonoBehaviour
{
    public Transform upPoint;
    public Transform downPoint;
    public float speed = 2f;

    private bool active = false;
    private Vector3 target;

    void Start()
    {
        target = upPoint.position;
    }

    void Update()
    {
        if (!active) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == upPoint.position) ? downPoint.position : upPoint.position;
        }
    }

    public void Activate()
    {
        active = true;
    }
}
