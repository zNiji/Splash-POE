using UnityEngine;

public class TheSun : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject fireballPrefab;
    public float maxTime = 1f;
    public float fireballSpeed = 10f; 
    private float currentTime = 1f;
    private Transform player;

    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        spawnLocation.LookAt(player);

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            var go = Instantiate(fireballPrefab, spawnLocation.position, spawnLocation.rotation);

            Rigidbody rb = go.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (player.position - spawnLocation.position).normalized;
                rb.velocity = direction * fireballSpeed;
            }
            currentTime = maxTime;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Boss took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Add death logic here (e.g., play animation, trigger event, destroy boss)
        Destroy(gameObject);
    }
}
