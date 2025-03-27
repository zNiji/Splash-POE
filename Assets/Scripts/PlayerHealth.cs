using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    public float maxHealth;
    public Image healthBar;
    public float drainRate = 2f;

    private bool isDead;
    public GameManager gameManager;

    private void Start()
    {
        maxHealth = health;
    }

    private void Update()
    {
        health -= drainRate * Time.deltaTime;

        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

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
            health = Mathf.Clamp(health, 0f, maxHealth);
            return true;
        }
        return false; // Returns false if no healing was needed
    }
}
