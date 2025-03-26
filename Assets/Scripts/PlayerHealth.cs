using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Image healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(maxHealth / currentHealth, 0, 1);
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
