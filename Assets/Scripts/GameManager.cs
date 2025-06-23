using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;
    public static GameManager Instance;
    public PointsSystem pointsSystem;
    public Database database;
    public PlayerProgress playerProgress;
    public PauseMenu pauseMenu;

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
        database = FindFirstObjectByType<Database>();
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

        if (Input.GetKey(KeyCode.Escape))
        {
            pauseMenu.Pause();
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
        resetStats();
        
        distance = -2;

        NormalRun = false;
        FastRun = false;

        if (pointsSystem.levelsCompleted >= 2)
        {
            RandomizeLevel();
        }
        else
        {
            LevelTwo = true;
        }
    }

    public async Task gameOver()
    {
        Time.timeScale = 0.0f;

        PlayerProgress playerProgress = await database.RetrievePlayerProgressAsync();

        if (playerProgress != null && playerProgress.points < pointsSystem.points)
        {
            database.StorePlayerProgress(pointsSystem.levelsCompleted, pointsSystem.points);
        }
        else if (playerProgress == null)
        { 
            database.StorePlayerProgress(pointsSystem.levelsCompleted, pointsSystem.points);
        }

        await SceneManager.LoadSceneAsync("Game Over");

        pointsSystem.UpdatePointsUIDeath();
    }

    public void restart()
    {
        distance = 0;
        pointsSystem.points = 0;
        pointsSystem.UpdatePointsUI();

        resetStats();

        GameOverUI.SetActive(false);
        
        Time.timeScale = 1.0f;
    }

    private void resetStats()
    {
        PlayerHealth.instance.health = 100;
        ControlSplash.instance.Speed = 20;
        PlayerHealth.instance.drainRate = 2.0f;
    }

    private void RandomizeLevel()
    {
        if (Random.Range(1, 11) >= 5)
        {
            LevelTwo = true;
        }
        else
        {
            LevelTwo = false;
        }
    }
}
