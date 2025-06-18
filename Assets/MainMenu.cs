using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Splash-POE");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
