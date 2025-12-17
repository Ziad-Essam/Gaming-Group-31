using UnityEngine;

public class SpawnKeyZ : MonoBehaviour
{
    public GameObject keyPrefab; 

    void OnDestroy()
    {
        if (gameObject.scene.isLoaded && keyPrefab != null)
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
        }
    }
}