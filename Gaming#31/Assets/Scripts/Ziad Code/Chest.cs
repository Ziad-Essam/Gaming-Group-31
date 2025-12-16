using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Visuals")]
    public Sprite openChestSprite; // Drag the OPEN chest image here
    
    [Header("Loot Settings")]
    public GameObject[] lootItems; // Drag your Coins/Potions PREFABS here
    public float scatterForce = 5f;

    private bool isOpened = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only trigger if Player touches it AND it's currently closed
        if (other.CompareTag("Player") && !isOpened)
        {
            // 1. Check Player Stats
            PlayerStats stats = other.GetComponent<PlayerStats>();

            // 2. Does player exist? Do they have the key?
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

        // Change the sprite to look open
        if (openChestSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = openChestSprite;
        }

        // Spawn all the loot items
        foreach (GameObject item in lootItems)
        {
            if (item != null)
            {
                GameObject loot = Instantiate(item, transform.position, Quaternion.identity);
                
                // Add a "Pop" effect if the loot has a Rigidbody
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