using UnityEngine;

public class WateringCanPickup : MonoBehaviour
{
    [SerializeField] private float healthAmount = 50f; // Amount of health to restore
    [SerializeField] private float rotationSpeed = 50f; // Speed of rotation effect

    private void Update()
    {
        // Rotate the pickup
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger");
            // Get the player's health component
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Try to heal the player and check if healing was successful
                bool wasHealed = playerHealth.Heal(healthAmount);

                if (wasHealed)
                {
                    // Destroy the pickup object
                    Destroy(gameObject);
                }
            }
        }
    }
}
