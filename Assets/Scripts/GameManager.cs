using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;
    public static GameManager Instance;
    public PointsSystem PointsSystem;

    [SerializeField] private ControlSplash controlPlayerSpeed;

    [SerializeField] private PlayerHealth controlPlayerHealth;

    [SerializeField] private GameObject persistantObjects;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver() 
    {
        GameOverUI.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameOverUI.SetActive(false);
        controlPlayerHealth.health = 100;
        controlPlayerSpeed.Speed = 20;
    }
}
