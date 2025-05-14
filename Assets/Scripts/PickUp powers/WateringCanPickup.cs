using UnityEngine;

public class WateringCanPickup : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f; // Speed of rotation effect

    private void Update()
    {
        // Rotate the pickup
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
