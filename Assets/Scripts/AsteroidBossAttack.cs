using UnityEngine;

public class AsteroidBossAttack : MonoBehaviour
{
    [SerializeField] float speed = 0;
    [SerializeField] float damageAmount = 10f; // Amount of health to deduct on hit
    [SerializeField] int pointsDeduction = 5; // Points to deduct on hit

    private void Update()
    {
        Vector3 forward = transform.forward;
        Vector3 direction = forward * speed;
        transform.position += direction * Time.deltaTime;
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ControlSplash>() != null)
        {
            // Get the PlayerHealth component
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Reduce player health
                playerHealth.health -= damageAmount;
                playerHealth.health = Mathf.Clamp(playerHealth.health, 0, playerHealth.maxHealth);
                playerHealth.healthBar.fillAmount = playerHealth.health / playerHealth.maxHealth;
            }

            // Get the PointsSystem component
            PointsSystem pointsSystem = FindObjectOfType<PointsSystem>();
            if (pointsSystem != null)
            {
                // Reduce player points
                int newPoints = Mathf.Max(0, pointsSystem.points - pointsDeduction); // Prevent negative points
                pointsSystem.points = newPoints;
                pointsSystem.UpdatePointsUI();
            }

            // Destroy the projectile after hitting the player
            Destroy(gameObject);
        }
    }
}
