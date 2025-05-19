using UnityEngine;

public class TheSun : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject fireballPrefab;
    public float maxTime = 1f;
    public float fireballSpeed = 10f;
    private float currentTime = 1f;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0, 20, 150);

    public float smoothSpeed = 0.5f; // value to control the speed of the boss's movement

    // Singleton instance
    public static TheSun instance;

    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public float followSpeed = 2f;

    void Start()
    {
        currentHealth = maxHealth;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            // If instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calculate the desired position of the boss
        Vector3 desiredPosition = player.transform.position + offset;

        // Smoothly move the boss towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Look at the player
        transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);

        // Adjust the rotation by 90 degrees to fix the firing direction
        transform.rotation *= Quaternion.Euler(0, -90, 0);

        // Update the spawn location to match the boss's position
        spawnLocation.position = transform.position;
        spawnLocation.rotation = transform.rotation;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            // Get the child empty object's transform
            Transform childEmptyObject = player.Find("Sun aiming point");

            if (childEmptyObject == null)
            {
                Debug.LogError("Child empty object not found.");
                return;
            }

            var go = Instantiate(fireballPrefab, spawnLocation.position, Quaternion.LookRotation(childEmptyObject.position - spawnLocation.position));

            Rigidbody rb = go.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 fireballDirection = (childEmptyObject.position - spawnLocation.position).normalized;
                rb.linearVelocity = fireballDirection * fireballSpeed;
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