using UnityEngine;
using System.Collections;
using System;

public class WaterGunShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign projectile prefab in Inspector
    public Transform firePoint; // Point where projectiles spawn (e.g., in front of player)
    public float projectileSpeed = 20f; // Speed of the projectile
    public float fireRate = 0.5f; // Time between shots (seconds)
    private bool isShooting = false; // Tracks if auto-shooting is active
    public event Action<float, float> OnShootingTimerUpdate; // Event to notify UI

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
            // Notify UI of remaining time
            OnShootingTimerUpdate?.Invoke(duration - elapsedTime, duration);
            yield return new WaitForSeconds(fireRate); // Wait for next shot
        }

        isShooting = false; // Stop shooting after 5 seconds
        OnShootingTimerUpdate?.Invoke(0f, duration); // Notify UI timer ended
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("ProjectilePrefab or FirePoint is not assigned!");
            return;
        }

        // Instantiate projectile at firePoint position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        // Get Rigidbody and apply force to shoot forward
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Ensure Rigidbody is not kinematic and has no constraints
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
            // Apply velocity in firePoint's forward direction
            rb.velocity = firePoint.forward * projectileSpeed;
            Debug.Log("Projectile fired with velocity: " + rb.velocity);
        }
        else
        {
            Debug.LogError("Projectile prefab missing Rigidbody!");
        }
    }
}