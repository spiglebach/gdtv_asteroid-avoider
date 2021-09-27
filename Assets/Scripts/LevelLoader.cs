using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void StartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RestartLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
