using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject GameOverLeaderboard;
    public Database database;


    [SerializeField] public TMP_Text pointsText;
    [SerializeField] public TMP_Text LevelsCompletedText;
    [SerializeField] private TMP_Text pointsTextDeath;

    void Start()
    {
        // Find the PointsSystem GameObject
        PointsSystem pointsSystem = FindObjectOfType<PointsSystem>();
        if (pointsSystem != null && pointsTextDeath != null)
        {
            pointsSystem.pointsTextDeath = pointsTextDeath; // Assign the TMP_Text
            pointsSystem.UpdatePointsUIDeath(); // Update the UI
        }
        else
        {
            Debug.LogError("PointsSystem or pointsTextDeath not found!");
        }
    }

    public void ShowGameOverScreen()
    {
        GameOverScreen.SetActive(true);
        GameOverLeaderboard.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync("Splash-POE");
        GameManager.Instance.player.transform.position = GameManager.Instance.spawnArea.transform.position;
        GameManager.Instance.player.transform.rotation = GameManager.Instance.spawnArea.transform.rotation;

        GameManager.Instance.restart();
    }

    public async void Leaderboard()
    {
        GameOverScreen.SetActive(false);
        GameOverLeaderboard.SetActive(true);

        PlayerProgress playerProgress = await database.RetrievePlayerProgressAsync();

        pointsText.text = "Highest Score: " + playerProgress.points.ToString();
        LevelsCompletedText.text = "Most bosses defeated: " + playerProgress.levelsCompleted.ToString();
    }
}
