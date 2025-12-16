using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    // --- PERSISTENT STATS (Shared across all scenes) ---
    public static int maxHealth = 100;
    public static int health = 100;
    public static int lives = 3;
    public static int score = 0;
    public static int fragments = 0;

    public bool hasTeleport = false;

    // --- HEALTH BAR (Inspector + Static Bridge) ---
    [Header("UI - Health")]
    public Image healthBar;                 // assign in Inspector
    public static Image HealthBarRef;        // static runtime reference

    // --- LEVEL SPECIFIC STATS ---
    public bool hasBossKey = false;

    [Header("UI - Fragments")]
    public GameObject fragmentIconPrefab;
    public Transform fragmentPanel;

    [Header("Effects")]
    private float flickerTime = 0f;
    public float flickerDuration = 0.1f;
    private SpriteRenderer sr;

    public bool isImmune = false;
    private float immunityTime = 0f;
    public float immunityDuration = 0.5f;

    [Header("UI - Text")]
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI livesUI;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Bridge Inspector  Static
        if (healthBar != null)
            HealthBarRef = healthBar;

        // Safety reset after game over
        if (lives <= 0)
        {
            lives = 3;
            maxHealth = 100;
            health = maxHealth;
            score = 0;
            fragments = 0;
        }

        // Restore fragment icons
        if (fragmentPanel != null && fragmentIconPrefab != null)
        {
            foreach (Transform child in fragmentPanel)
                Destroy(child.gameObject);

            for (int i = 0; i < fragments; i++)
                Instantiate(fragmentIconPrefab, fragmentPanel);
        }

        UpdateHealthBar();
    }

    // --- FRAGMENTS ---
    public void AddFragment()
    {
        fragments++;

        if (fragmentIconPrefab != null && fragmentPanel != null)
            Instantiate(fragmentIconPrefab, fragmentPanel);
    }

    // --- DAMAGE ---
    public void TakeDamage(int damage)
    {
        if (isImmune) return;

        health -= damage;
        if (health < 0) health = 0;

        UpdateHealthBar();

        if (lives > 0 && health == 0)
        {
            if (FindObjectOfType<LevelManager>() != null)
                FindObjectOfType<LevelManager>().RespawnPlayer();

            health = maxHealth;
            lives--;
            UpdateHealthBar();
        }
        else if (lives == 0 && health == 0)
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
        }

        Debug.Log("Player Health: " + health);
        Debug.Log("Player Lives: " + lives);

        isImmune = true;
        immunityTime = 0f;
    }

    // --- HEALTH BAR UPDATE ---
    void UpdateHealthBar()
    {
        if (HealthBarRef != null)
            HealthBarRef.fillAmount = (float)health / maxHealth;
    }

    // --- VISUAL DAMAGE EFFECT ---
    void SpriteFlicker()
    {
        if (flickerTime < flickerDuration)
        {
            flickerTime += Time.deltaTime;
        }
        else
        {
            sr.enabled = !sr.enabled;
            flickerTime = 0;
        }
    }

    void Update()
    {
        if (isImmune)
        {
            SpriteFlicker();
            immunityTime += Time.deltaTime;

            if (immunityTime >= immunityDuration)
            {
                isImmune = false;
                sr.enabled = true;
            }
        }

        if (scoreUI != null)
            scoreUI.text = "Score: " + score;

        if (livesUI != null)
            livesUI.text = "Lives: " + lives + " | HP: " + health + "/" + maxHealth;
    }
}
