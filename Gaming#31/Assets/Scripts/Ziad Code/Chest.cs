using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Visuals")]
    public Sprite openChestSprite; 
    
    [Header("Loot Settings")]
    public GameObject[] lootItems; 
    public float scatterForce = 5f;

    private bool isOpened = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null && stats.hasBossKey == true)
            {
                OpenChest();
            }
            else
            {
                Debug.Log("Locked! You need the Boss Key.");
            }
        }
    }

    void OpenChest()
    {
        isOpened = true;
        Debug.Log("Chest Opened!");

        if (openChestSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = openChestSprite;
        }

        foreach (GameObject item in lootItems)
        {
            if (item != null)
            {
                GameObject loot = Instantiate(item, transform.position, Quaternion.identity);
                
                Rigidbody2D rb = loot.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 randomDir = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f));
                    rb.AddForce(randomDir.normalized * scatterForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}