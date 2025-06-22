using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Splash-POE");
        Time.timeScale = 1f;
        GameManager.Instance.player.transform.position = GameManager.Instance.spawnArea.transform.position;
        GameManager.Instance.player.transform.rotation = GameManager.Instance.spawnArea.transform.rotation;

        GameManager.Instance.restart();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
