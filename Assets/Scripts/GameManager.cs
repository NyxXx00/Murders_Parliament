using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            }
            else Pause();
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void OptionMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("OptionsPanel");
    }

    public void QuitGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
