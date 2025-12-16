using UnityEngine;

public class SpawnKeyZ : MonoBehaviour
{
    public GameObject keyPrefab; // Drag your Key PREFAB here

    void OnDestroy()
    {
        // Only spawn if the scene is actually running (prevents errors when quitting)
        if (gameObject.scene.isLoaded && keyPrefab != null)
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
        }
    }
}