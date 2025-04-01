using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100; 
    public Image healthBar;
    public float drainRate = 2f;

    private bool isDead;
    public GameManager gameManager;

    private void Start()
    {
        maxHealth = 100; // Ensure maxHealth is 100
        health = Mathf.Clamp(health, 0, maxHealth); // Clamp initial health
    }

    private void Update()
    {
        health -= drainRate * Time.deltaTime;
        health = Mathf.Clamp(health, 0, maxHealth); // Prevent health from going below 0 or above 100

        healthBar.fillAmount = health / maxHealth;

        if (health <= 0 && !isDead)
        {
            isDead = true;
            gameManager.gameOver();
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

    
}
