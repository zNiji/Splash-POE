using Unity.VisualScripting;
using UnityEngine;

public class Splashstatus : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnPickupCollected.AddListener(HandlePickup);
    }

    private void HandlePickup(PickupType pickupType)
    {
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();

        switch (pickupType)
        {
            case PickupType.WateringCan:
                float healthAmount = 50f;
                if (playerHealth != null)
                {
                    bool wasHealed = playerHealth.Heal(healthAmount);
                    if (wasHealed)
                    {
                        // No need to destroy the pickup, it's already handled in the Pickup script
                    }
                }
                break;
            case PickupType.Umbrella:
                float pauseDuration = 5f;
                if (playerHealth != null)
                {
                    playerHealth.PauseHealthDrain(pauseDuration);
                }
                break;
            // Add more cases for each pickup type
        }
    }
}