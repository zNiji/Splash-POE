using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    public Image healthBar;
    public float drainRate = 2f;

    private bool isDead;
    public GameManager gameManager;
    private bool isDrainPaused;
    private float pauseTimer;
    private float pauseDuration; // Store total duration for UI normalization
    public event Action<float, float> OnPauseTimerUpdate; // Event to notify UI

    public static PlayerHealth instance;

    void Awake()
    {
        // Check if the instance is already created
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate instance
        }
    }

    private void Start()
    {
        maxHealth = 100; // Ensure maxHealth is 100
        health = Mathf.Clamp(health, 0, maxHealth); // Clamp initial health
    }

    private void Update()
    {
        // Only drain health if not paused
        if (!isDrainPaused)
        {
            health -= drainRate * Time.deltaTime;
        }
        else
        {
            // Handle pause timer
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                isDrainPaused = false; // Resume draining when timer expires
                pauseTimer = 0f;
            }
            // Notify UI of remaining time
            OnPauseTimerUpdate?.Invoke(pauseTimer, pauseDuration);
        }

        health = Mathf.Clamp(health, 0, maxHealth); // Prevent health from going below 0 or above 100
        healthBar.fillAmount = health / maxHealth;

        if (health <= 0 && !isDead)
        {
            isDead = true;
            gameManager.gameOver().Wait();
        }
    }

    public bool Heal(float amount)
    {
        // Only heals if not at max health
        if (health < maxHealth)
        {
            health += amount;
            health = Mathf.Clamp(health, 0, maxHealth); // Cap health at 100
            healthBar.fillAmount = health / maxHealth;
            return true;
        }
        return false; // Returns false if no healing was needed
    }

    public void PauseHealthDrain(float duration)
    {
        isDrainPaused = true;
        pauseTimer = duration;
        pauseDuration = duration; // Store for UI
        // Notify UI to show timer
        OnPauseTimerUpdate?.Invoke(pauseTimer, pauseDuration);
    }
}