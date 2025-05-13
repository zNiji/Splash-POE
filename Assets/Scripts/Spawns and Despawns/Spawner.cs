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
        for (int i = 0; i < 35; i++)
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
                // Perform the box cast
                Vector3 boxCenter = new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ);
                Vector3 boxSize = renderer.bounds.size;
                Quaternion boxRotation = Quaternion.identity;

                RaycastHit[] hits = Physics.BoxCastAll(boxCenter, boxSize * 2, Vector3.zero, boxRotation, 0f, LayerMask.GetMask("ObjectLayer"));

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
                spawnPositionValid = true;
            }
        }

        // Check if an obstacle is already spawned at this position
        if (obstacle != null && Vector3.Distance(obstacle.transform.position, new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ)) < 0.1f)
        {
            DestroyObstacle();
        }

        // Instantiate the obstacle at the random position
        if (obstacleIndex == 0)
        {
            obstacle = Instantiate(obstaclePrefab, new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), Quaternion.Euler(-90, 0, 0));
        }
        else
        {
            obstacle = Instantiate(obstaclePrefab, new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), Quaternion.identity);
        }
    }

    public void DestroyObstacle()
    {
        if (obstacle != null)
        {
            Debug.Log("Destroyed obstacle");
            Destroy(obstacle);
            obstacle = null;
        }
    }
}