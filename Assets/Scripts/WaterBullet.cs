using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    public float damage = 10f; // Damage to deal to the boss

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the projectile hits the boss
        if (collision.gameObject.CompareTag("Boss"))
        {
            // Apply damage to the boss
            TheSun bossHealth = collision.gameObject.GetComponent<TheSun>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamageSun(damage);
            }
        }
        else if (collision.gameObject.CompareTag("Moon"))
        {
            TheMoon moonHealth = collision.gameObject.GetComponent<TheMoon>();
            if (moonHealth != null)
            {
                moonHealth.TakeDamageMoon(damage); // Assuming the method is called TakeDamageMoon
            }
        }

        // Destroy the projectile on collision
        Destroy(gameObject);
    }

    void Start()
    {
        Destroy(gameObject, 10f); // Destroy after 10 seconds
    }
}
