using UnityEngine;

public class WaterGunPickup : MonoBehaviour
{
    public float rotationSpeed = 50f; // Speed of pickup rotation

    void Update()
    {
        // Rotate the pickup for visual effect
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the pickup
        if (other.CompareTag("Player"))
        {
            // Enable shooting for the player
            WaterGunShooting playerShooting = other.GetComponent<WaterGunShooting>();
            if (playerShooting != null)
            {
                playerShooting.EnableShooting();
            }
            // Destroy the pickup
            Destroy(gameObject);
        }
    }
}
