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
        GameManager.Instance.player.transform.position = GameManager.Instance.spawnArea.transform.position;
        GameManager.Instance.player.transform.rotation = GameManager.Instance.spawnArea.transform.rotation;

        GameManager.Instance.restart();
    }
}
