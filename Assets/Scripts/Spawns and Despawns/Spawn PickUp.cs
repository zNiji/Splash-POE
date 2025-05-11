using UnityEngine;
using static EObstacles;

public class SpawnPickUp : MonoBehaviour
{
    [SerializeField] private SpawnableType[] SpawnableTypes;
    [SerializeField] private GameObject[] pickUpPrefabs;

    private int pickUpIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        pickUpIndex = Random.Range(0, pickUpPrefabs.Length);
        GameObject pickupToSpawn = pickUpPrefabs[pickUpIndex];
        // Spawn the pickup game object
        Instantiate(pickupToSpawn, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
