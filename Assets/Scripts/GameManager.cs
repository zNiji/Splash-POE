using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;
    public static GameManager Instance;
    public PointsSystem PointsSystem;

    public Animator animator;
    public bool NormalRun = false;
    public bool FastRun = false;

    public int distance = 0;

    [SerializeField] private ControlSplash controlPlayerSpeed;

    [SerializeField] private PlayerHealth controlPlayerHealth;

    [SerializeField] private GameObject persistantObjects;

    public bool spawn = false;

    void Awake()
    {
        // Check if the instance is already created
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy the singleton instance when the scene is loaded
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

        if (persistantObjects == null)
        {
            persistantObjects = GameObject.Find("PersistantObjects");
            DontDestroyOnLoad(persistantObjects);
        }
    }

    private void FixedUpdate()
    {
        if (distance == 2)
        {
            spawn = true;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
            // default state, e.g. idle
            animator.SetBool("NormalRun", false);
            animator.SetBool("FastRun", false);
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

        if (spawn) 
        { 
            SpawnBoss.instance.BossSpawner();
            spawn = false;
        }

    }

    public void gameOver()
    {
        Time.timeScale = 0.0f;
        GameOverUI.SetActive(true);
        PointsSystem.pointsTextDeath.text = "Final " + PointsSystem.pointsText.text;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameOverUI.SetActive(false);
        controlPlayerHealth.health = 100;
        controlPlayerSpeed.Speed = 20;
        Time.timeScale = 1.0f;
    }
}
