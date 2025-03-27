using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    public float drainRate = 2f;

    private bool isDead;
    public GameManager gameManager;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        currentHealth -= drainRate * Time.deltaTime;

        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            gameManager.gameOver();

        }
    }

    public bool Heal(float amount)
    {
        // Only heals if not at max health
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            return true;
        }
        return false; // Returns false if no healing was needed
    }
}
