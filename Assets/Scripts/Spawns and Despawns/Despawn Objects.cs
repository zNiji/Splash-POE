using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the inspector

    void LateUpdate()
    {
        transform.position = player.position;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the exiting object has the "obstacle" tag
        if (other.gameObject.tag == "Obstacle")
        {
            // Destroy the obstacle
            Destroy(other.gameObject);
        }
    }
}