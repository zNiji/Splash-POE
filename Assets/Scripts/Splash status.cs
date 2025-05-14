using Unity.VisualScripting;
using UnityEngine;

public class Splashstatus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the pick up
        if (other.GetComponent<WateringCanPickup>() != null)
        {
            
            PlayerHealth playerHealth = GetComponent<PlayerHealth>(); // Get the player's health component
            float healthAmount = 50f; // Amount of health to restore

            if (playerHealth != null)
            {
                // Try to heal the player and check if healing was successful
                bool wasHealed = playerHealth.Heal(healthAmount);

                if (wasHealed)
                {
                    // Destroy the pickup object
                    Destroy(other.gameObject);
                }
            }
        }
        
    }
}
