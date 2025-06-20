using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject GamePauseScreen;
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void Pause()
    {
        GamePauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        GamePauseScreen.SetActive(false);
        Time.timeScale = 1f;

    }
}
