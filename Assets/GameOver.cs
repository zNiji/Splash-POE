using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync("Splash-POE");
    }
}
