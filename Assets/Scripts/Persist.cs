using UnityEngine;
using UnityEngine.SceneManagement;

public class Persist : MonoBehaviour
{
    private Canvas persistenteCanvas;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        persistenteCanvas = GetComponent<Canvas>();
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (persistenteCanvas != null) {

            persistenteCanvas.gameObject.SetActive(false);
            persistenteCanvas.gameObject.SetActive(true);

        }
    }
}
