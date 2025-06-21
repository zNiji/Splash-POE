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
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Start()
    {
        GameManager.Instance.Spawn.AddListener(BossSpawner);
    }

    void Update()
    {
        if (player != null)
        {
            transform.LookAt(player.transform.position);
        }
    }

    public void BossSpawner()
    {
        Instantiate(bossPrefab, player.transform.position + offset, Quaternion.identity);
    }
}