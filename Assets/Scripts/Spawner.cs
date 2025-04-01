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

    private void Awake()
    {
        // Initialize obstacle index
        obstacleIndex = UnityEngine.Random.Range(0, SpawnableTypes.Length);

        Spawn();
    }

    private void Spawn()
    {
        // Get the obstacle prefab based on the obstacle index
        GameObject obstaclePrefab = SpawnablePrefabs[obstacleIndex];

        // Instantiate the obstacle at the spawn point
        obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
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