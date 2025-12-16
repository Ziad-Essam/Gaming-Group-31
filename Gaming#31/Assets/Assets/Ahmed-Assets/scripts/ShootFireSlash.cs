using UnityEngine;

public class ShootFireSlash : MonoBehaviour
{
    public GameObject fireSlashPrefab;
    public Transform firePoint;

    public void ShootFireSlashAttack()
    {
        Vector3 dir = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
        firePoint.right = dir;

        Instantiate(
            fireSlashPrefab,
            firePoint.position,
            firePoint.rotation
        );
    }
}