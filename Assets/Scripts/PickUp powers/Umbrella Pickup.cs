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
        // Rotate based on the pickup type
        if (gameObject.name == "Umbrella")
        {
            // Rotate Umbrella on the Y-axis
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else if (gameObject.name == "Sunhat")
        {
            // Rotate Sunhat on the Z-axis
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Shield");
            playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.PauseHealthDrain(pauseDuration);
                Destroy(gameObject); // Destroy pickup after collection
            }
        }
    }
}
