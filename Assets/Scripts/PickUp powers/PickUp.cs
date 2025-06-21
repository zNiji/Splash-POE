using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupType pickupType; // Enum to identify the type of pickup

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Trigger the OnPickupCollected event
            GameManager.Instance.OnPickupCollected.Invoke(pickupType);
            Destroy(gameObject);
        }
    }
}

public enum PickupType
{
    WateringCan,
    Umbrella,
    None
    // Add more pickup types here
}