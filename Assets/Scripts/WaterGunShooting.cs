using UnityEngine;
using System.Collections;

public class WaterGunShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign projectile prefab in Inspector
    public Transform firePoint; // Point where projectiles spawn (e.g., in front of player)
    public float projectileSpeed = 100f; // Speed of the projectile
    public float fireRate = 0.5f; // Time between shots (seconds)
    private bool isShooting = false; // Tracks if auto-shooting is active

    // Called when player collects the pickup
    public void EnableShooting()
    {
        if (!isShooting) // Prevent restarting if already shooting
        {
            StartCoroutine(AutoShoot());
        }
    }

    IEnumerator AutoShoot()
    {
        isShooting = true;
        float duration = 5f; // Shoot for 5 seconds
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Shoot();
            elapsedTime += fireRate;
            yield return new WaitForSeconds(fireRate); // Wait for next shot
        }

        isShooting = false; // Stop shooting after 5 seconds
    }

    void Shoot()
    {
        // Instantiate projectile at firePoint position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        // Get Rigidbody and apply force to shoot forward
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * projectileSpeed;
        }
    }
}
