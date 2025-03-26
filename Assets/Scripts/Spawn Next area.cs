using UnityEngine;

public class SpawnNextarea : MonoBehaviour
{
    [SerializeField] private GameObject PlayerTrigger;

    [SerializeField] private Transform SpawnGroundHere;

    [SerializeField] private Transform SpawnGround;

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
        if (other.CompareTag("Player"))
        {
            Instantiate(SpawnGround, SpawnGroundHere.position, Quaternion.identity);
        }
    }
}
