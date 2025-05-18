using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab;
    [SerializeField] private Vector3 offset = new Vector3(0, 30, 220);
    [SerializeField] private GameObject player;

    public static SpawnBoss instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.LookAt(player.transform.position);
    }

    public void BossSpawner()
    {
        Instantiate(bossPrefab, player.transform.position + offset, Quaternion.identity);
    }
}