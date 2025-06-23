using System;
using Unity.VisualScripting;
using UnityEngine;
using static EObstacles;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnableType[] SpawnableTypes;
    [SerializeField] private GameObject[] SpawnablePrefabs;

    private GameObject obstacle;

    private int obstacleIndex;

    private float spawnPositionX;
    private float spawnPositionY;
    private float spawnPositionZ;

    private void Awake()
    {
        DestroyOverlappingObstacles(transform.position);
        for (int i = 0; i < 30; i++)
        {
            obstacleIndex = UnityEngine.Random.Range(0, SpawnableTypes.Length);
            Spawn();
        }
    }

    private void Spawn()
    {
        // Check if SpawnableTypes and SpawnablePrefabs arrays are not empty
        if (SpawnableTypes.Length == 0 || SpawnablePrefabs.Length == 0)
        {
            Debug.LogError("SpawnableTypes or SpawnablePrefabs arrays are empty");
            return;
        }

        // Get the obstacle prefab based on the obstacle index
        GameObject obstaclePrefab = SpawnablePrefabs[obstacleIndex];

        // Define the spawn area along the length of the ground
        float spawnAreaLength = 250f; // Roughly the same as the z dimension of the ground
        float spawnAreaWidth = 23f; // Roughly the same as the x dimension of the ground

        bool spawnPositionValid = false;

        while (!spawnPositionValid)
        {
            // Generate a random position along the length of the ground
            Vector3 groundPosition = transform.parent.position;

            spawnPositionX = groundPosition.x + UnityEngine.Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
            spawnPositionY = groundPosition.y;
            spawnPositionZ = groundPosition.z + UnityEngine.Random.Range(-230, spawnAreaLength);

            // Check if the spawn position is already occupied by another object
            Renderer renderer = obstaclePrefab.GetComponent<Renderer>();
            if (renderer == null)
            {
                renderer = obstaclePrefab.GetComponentInChildren<Renderer>();
            }

            if (renderer != null)
            {
                // Get the collider component from the obstacle prefab
                Collider collider = obstaclePrefab.GetComponent<Collider>();

                if (collider != null)
                {
                    // Perform the box cast
                    Vector3 boxCenter = new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ);
                    Vector3 boxSize = collider.bounds.size;
                    Quaternion boxRotation = Quaternion.identity;

                    RaycastHit[] hits = Physics.BoxCastAll(boxCenter, boxSize, Vector3.zero, boxRotation, 0f, LayerMask.GetMask("ObjectLayer"));

                    // Check if any objects were hit
                    if (hits.Length > 0)
                    {
                        // If an object was hit, try a different spawn position
                        spawnPositionValid = false;
                    }
                    else
                    {
                        // If no objects were hit, the spawn position is valid
                        spawnPositionValid = true;
                    }
                }
                else
                {
                    Debug.LogError("No collider component found on the obstacle prefab.");
                    spawnPositionValid = true;
                }
            }
            else
            {
                spawnPositionValid = true;
            }
        }

        // Check if an obstacle is already spawned at this position
        if (obstacle != null && Vector3.Distance(obstacle.transform.position, new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ)) < 0.1f)
        {
            DestroyObstacle();
        }

        DestroyOverlappingObstacles(new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ));

        //// Instantiate the obstacle at the random position
        //if (obstacleIndex == 0)
        //{
        //    obstacle = Instantiate(obstaclePrefab, new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), Quaternion.identity);
        //}
        //else
        //{
        //    obstacle = Instantiate(obstaclePrefab, new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), Quaternion.identity);
        //}

        switch (SpawnableTypes[obstacleIndex])
        {
            case SpawnableType.Rock:
                obstacle = Instantiate(SpawnablePrefabs[obstacleIndex], new Vector3(spawnPositionX, spawnPositionY + 0.3f, spawnPositionZ), Quaternion.Euler(-90, 0, 0));
                break;

            case SpawnableType.Tree:
                obstacle = Instantiate(SpawnablePrefabs[obstacleIndex], new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), Quaternion.identity);
                break;

            case SpawnableType.Fence:
                obstacle = Instantiate(SpawnablePrefabs[obstacleIndex], new Vector3(spawnPositionX, spawnPositionY + 0.4f, spawnPositionZ), Quaternion.identity);
                break;

            case SpawnableType.wateringCan:
                obstacle = Instantiate(SpawnablePrefabs[obstacleIndex], new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), Quaternion.identity);
                break;
            
            case SpawnableType.WaterGun:
                obstacle = Instantiate(SpawnablePrefabs[obstacleIndex], new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), Quaternion.identity);
                break;
            
            case SpawnableType.Umbrella:
                obstacle = Instantiate(SpawnablePrefabs[obstacleIndex], new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), Quaternion.identity);
                break;
            
            case SpawnableType.Cactus:
                obstacle = Instantiate(SpawnablePrefabs[obstacleIndex], new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), Quaternion.identity);
                break;
            
            case SpawnableType.Crate:
                obstacle = Instantiate(SpawnablePrefabs[obstacleIndex], new Vector3(spawnPositionX, spawnPositionY + 1.2f, spawnPositionZ), Quaternion.identity);
                break;
            
            case SpawnableType.DesertRock:
                obstacle = Instantiate(SpawnablePrefabs[obstacleIndex], new Vector3(spawnPositionX, spawnPositionY + 2f, spawnPositionZ), Quaternion.Euler(0, 0, -90));
                break;
            
            case SpawnableType.SunHat:
                obstacle = Instantiate(SpawnablePrefabs[obstacleIndex], new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), Quaternion.Euler(0, 0, 0));
                break;
            
            case SpawnableType.WaterBottle:
                obstacle = Instantiate(SpawnablePrefabs[obstacleIndex], new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), Quaternion.identity);
                break;
            
            case SpawnableType.None:
                Debug.LogError("Unknown SpawnableType");
                return;
            
            default:
                Debug.LogError("Unknown SpawnableType");
                return;
        }
    }

    public void DestroyObstacle()
    {
        if (obstacle != null)
        {
            Debug.Log("Destroyed obstacle");
            DestroyObstacleRecursive(obstacle);
            obstacle = null;
        }
    }

    private void DestroyObstacleRecursive(GameObject obstacle)
    {
        // Destroy the obstacle
        Destroy(obstacle);

        // Recursively destroy its parents
        if (obstacle.transform.parent != null)
        {
            DestroyObstacleRecursive(obstacle.transform.parent.gameObject);
        }
    }

    private void DestroyOverlappingObstacles(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, 0.1f);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.tag == "Obstacle")
            {
                Debug.Log("Found overlapping obstacle: " + hit.gameObject.name);
                Destroy(hit.gameObject);
            }
        }
    }
}