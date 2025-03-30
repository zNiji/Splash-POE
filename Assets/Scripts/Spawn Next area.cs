using UnityEngine;

public class SpawnNextArea : MonoBehaviour
{
    [SerializeField] private Transform SpawnGroundHere; // Point to spawn the ground

    [SerializeField] private GameObject SpawnableGround; // prefab of the ground to spawn

    private static GameObject currentGround; // current ground in the scene
    private static GameObject previousGround; // previous ground in the scene

    [SerializeField] private ControlSplash controlSplash;  // Reference to the ControlSplash script through the player

    private bool isSpawning = false; // Flag to check if the script is currently spawning a new ground


    void Awake()
    {
        // Checks if the instance is already created
        if (currentGround == null)
        {
            currentGround = GameObject.FindWithTag("Ground"); // Find the current ground in the scene by its tag
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the player has entered the trigger
        if (other.CompareTag("Player") && !isSpawning)
        {
            isSpawning = true;

            // Spawns the next ground
            GameObject nextGround = Instantiate(SpawnableGround, SpawnGroundHere.position, Quaternion.identity);

            // Updates the previous ground reference
            previousGround = currentGround;

            // Updates the current ground reference
            currentGround = nextGround;

            controlSplash.Speed += 10.0f; // Increase the speed of splash for difficulty

            isSpawning = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Checks if the player has exited the trigger
        if (other.CompareTag("Player"))
        {
            // Delete the previous ground
            if (previousGround != null)
            {
                Destroy(previousGround);
            }
        }
    }
}