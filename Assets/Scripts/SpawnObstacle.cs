using System;
using Unity.VisualScripting;
using UnityEngine;
using static EObstacles;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] private ObstacleType[] obstacleTypes;
    [SerializeField] private GameObject[] obstaclePrefabs;

    private int obstacleIndex;

    private void Awake()
    {
        // Initialize obstacle index
        obstacleIndex = UnityEngine.Random.Range(0, obstacleTypes.Length);

        Spawn();
    }

    private void Spawn()
    {
        // Get the obstacle prefab based on the obstacle index
        GameObject obstaclePrefab = obstaclePrefabs[obstacleIndex];

        // Instantiate the obstacle at the spawn point
        GameObject obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
    }
}