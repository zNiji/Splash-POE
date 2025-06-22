using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuScreen;
    public GameObject MainMenuLeaderboard;
    public Database database;

    [SerializeField] public TMP_Text pointsText;
    [SerializeField] public TMP_Text LevelsCompletedText;

    public void Start()
    {
        database = FindFirstObjectByType<Database>();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Splash-POE");
        Time.timeScale = 1f;
        GameManager.Instance.player.transform.position = GameManager.Instance.spawnArea.transform.position;
        GameManager.Instance.player.transform.rotation = GameManager.Instance.spawnArea.transform.rotation;
        PointsSystem pointsSystem = FindFirstObjectByType<PointsSystem>();
        if (pointsSystem != null)
        {
            pointsSystem.ResetPoints(); // Reset the points
        }
        GameManager.Instance.restart();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public async void Leaderboard()
    {
        MainMenuScreen.SetActive(false);
        MainMenuLeaderboard.SetActive(true);

        PlayerProgress playerProgress = await database.RetrievePlayerProgressAsync();

        pointsText.text = "Highest Score: " + playerProgress.points.ToString();
        LevelsCompletedText.text = "Most bosses defeated: " + playerProgress.levelsCompleted.ToString();
    }

    public void ShowMainMenu()
    {
        MainMenuScreen.SetActive(true);
        MainMenuLeaderboard.SetActive(false);
    }
}
