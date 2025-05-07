using UnityEngine;

public class UmbrellaPickup : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f; // Speed of rotation effect
    public float pauseDuration = 5f;
    private PlayerHealth playerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the pickup
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.PauseHealthDrain(pauseDuration);
                Destroy(gameObject); // Destroy pickup after collection
            }
        }
    }
}
