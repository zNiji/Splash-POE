using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;
    public static GameManager Instance;
    public PointsSystem PointsSystem;

    public Animator animator;
    public bool NormalRun = false;
    public bool FastRun = false;

    public bool LevelTwo = false;

    public int distance = 0;

    [SerializeField] private GameObject persistantObjects;

    public GameObject player;

    public GameObject spawnArea;

    public bool spawner = false;

    public UnityEvent<PickupType> OnPickupCollected;

    public UnityEvent ObstaclePassed;

    public UnityEvent Spawn;

    public UnityEvent LevelComplete;

    void Awake()
    {
        // Check if the instance is already created
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate instance
        }

        if (GameOverUI == null)
        {
            GameOverUI = GameObject.FindWithTag("GameOverScreen");
            DontDestroyOnLoad(GameOverUI);
        }

        animator = player.GetComponent<Animator>();
    }

    void Start()
    {
        persistantObjects = GameObject.Find("PersistantObjects");
        persistantObjects.transform.SetParent(null); // Make sure it's a root object
        DontDestroyOnLoad(persistantObjects);
    }

    private void FixedUpdate()
    {
        if (distance == 2)
        {
            spawner = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            if (NormalRun)
            {
                animator.SetBool("NormalRun", true);
                animator.SetBool("FastRun", false);
            }
            else if (FastRun)
            {
                animator.SetBool("NormalRun", false);
                animator.SetBool("FastRun", true);
            }
            else
            {
                // default state
                animator.SetBool("NormalRun", false);
                animator.SetBool("FastRun", false);
            }
        }
    }

    public void RunFaster()
    {
        distance++;

        if (distance == 2)
        {
            FastRun = true;
            NormalRun = false;
        }
        else
        {
            FastRun = false;
            NormalRun = true;
        }

        if (spawner)
        {
            Spawn.Invoke();
            spawner = false;
        }

    }

    public void LevelChange()
    {
        PlayerHealth.instance.health = 100;
        ControlSplash.instance.Speed = 20;

        LevelTwo = true;
        NormalRun = false;
        FastRun = false;
    }

    public void gameOver()
    {
        Time.timeScale = 0.0f;

        SceneManager.LoadSceneAsync("Game Over");

        PointsSystem.UpdatePointsUIDeath();
    }

    public void restart()
    {
        PointsSystem.points = 0;
        PointsSystem.UpdatePointsUI();

        GameOverUI.SetActive(false);
        PlayerHealth.instance.health = 100;
        ControlSplash.instance.Speed = 20;
        Time.timeScale = 1.0f;
    }
}
