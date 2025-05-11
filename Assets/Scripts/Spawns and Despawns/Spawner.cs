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
        float spawnAreaLength = 250f; // same as the z dimension of the ground
        float spawnAreaWidth = 23f; // same as the x dimension of the ground

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
                int groundLayer = LayerMask.NameToLayer("ground");
                Collider[] colliders = new Collider[10];
                int count = Physics.OverlapSphereNonAlloc(new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), renderer.bounds.size.x / 2, colliders, ~groundLayer);
                if (count == 0)
                {
                    spawnPositionValid = true;
                }
            }

            else
            {
                spawnPositionValid = true;
            }
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
            Destroy(obstacle);
            obstacle = null;
        }
    }
}