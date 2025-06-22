using UnityEngine;

public class SpawnNextArea : MonoBehaviour
{
    [SerializeField] private Transform SpawnGroundHere; // Point to spawn the ground

    [SerializeField] private GameObject SpawnableGround; // prefab of the ground to spawn

    [SerializeField] private GameObject SpawnableGroundTwo; // prefab of the sand ground to spawn

    [SerializeField] private GameObject emptyGround; // prefab of the boss ground to spawn

    [SerializeField] private GameObject emptyGroundSand; // prefab of the boss sand ground to spawn


    private static GameObject currentGround; // current ground in the scene
    private static GameObject previousGround; // previous ground in the scene

    public ControlSplash controlSplash;  // Reference to the ControlSplash script through the player

    public PlayerHealth controlPlayerHealth;

    private bool isSpawning = false; // Flag to check if the script is currently spawning a new ground

    void Awake()
    {
        // Checks if the instance is already created
        if (currentGround == null)
        {
            currentGround = GameObject.FindWithTag("Ground"); // Find the current ground in the scene by its tag
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controlSplash == null)
        {
            controlSplash = GameObject.FindGameObjectWithTag("Player").GetComponent<ControlSplash>();
            controlPlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the player has entered the trigger
        if (other.CompareTag("Player") && !isSpawning)
        {
            isSpawning = true;

            GameObject nextGround;

            // Find all spawners
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");

            // Loop through each spawner and destroy the obstacle
            foreach (GameObject spawner in spawners)
            {
                Spawner spawnerPrefab = spawner.GetComponent<Spawner>();
                spawnerPrefab.DestroyObstacle();
            }

            if (GameManager.Instance.distance > 2)
            {
                // Spawns the next ground
                if (GameManager.Instance.LevelTwo)
                {
                    nextGround = Instantiate(emptyGroundSand, SpawnGroundHere.position, Quaternion.identity);
                }
                else
                {
                    nextGround = Instantiate(emptyGround, SpawnGroundHere.position, Quaternion.identity);
                }
            }
            else
            {
                // Spawns the next ground
                if (GameManager.Instance.LevelTwo)
                {
                    nextGround = Instantiate(SpawnableGroundTwo, SpawnGroundHere.position, Quaternion.identity);
                }
                else
                {
                    nextGround = Instantiate(SpawnableGround, SpawnGroundHere.position, Quaternion.identity);
                }
            }

            // Updates the previous ground reference
            previousGround = currentGround;

            // Updates the current ground reference
            currentGround = nextGround;

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

            if(GameManager.Instance.distance <= 2 && GameManager.Instance.distance >= 0)
            {
                controlSplash.Speed += 10.0f; // Increase the speed of splash for difficulty

                controlPlayerHealth.drainRate += 1.0f;

            }
            GameManager.Instance.RunFaster();
        }
    }
}