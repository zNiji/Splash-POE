using UnityEngine;

public class WaterGunPickup : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private GameObject WaterBulletPrefab;
    [SerializeField] public Transform shootPoint;
    [SerializeField] private float shootInterval = 0.5f;
    [SerializeField] private float projectileSpeed = 15f;
    [SerializeField] private float projectileLifetime = 3f;
    [SerializeField] private float damagePerShot = 10f;
    private float shootTimer = 0f;
    private bool isEquipped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEquipped = true;
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        if (!isEquipped) return;

        // Handle automatic shooting
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the shoot point
        GameObject projectile = Instantiate(WaterBulletPrefab, shootPoint.position, shootPoint.rotation);

        // Get the Rigidbody and set velocity forward
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootPoint.forward * projectileSpeed;
        }

        // Attach damage information to the projectile
        WaterBullet projectileScript = projectile.GetComponent<WaterBullet>();
        if (projectileScript != null)
        {
            projectileScript.damage = damagePerShot;
        }

        // Destroy the projectile after its lifetime
        Destroy(projectile, projectileLifetime);
    }
}
