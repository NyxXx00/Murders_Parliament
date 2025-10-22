using UnityEngine;

public class MenuOpciones : MonoBehaviour {
    public GameObject optionsPanel;

    public void OpenOptions() {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions() {
        optionsPanel.SetActive(false);
    }

    public void SetVolume(float volume) {
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }
}
