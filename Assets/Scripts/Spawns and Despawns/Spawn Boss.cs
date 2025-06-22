using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] GameObject bossPrefabSun;
    [SerializeField] GameObject bossPrefabMoon;
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
        if (GameManager.Instance.LevelTwo)
        {
            Instantiate(bossPrefabMoon, player.transform.position + offset, Quaternion.identity);

        }
        else 
        {
            Instantiate(bossPrefabSun, player.transform.position + offset, Quaternion.identity);
        }
    }
}