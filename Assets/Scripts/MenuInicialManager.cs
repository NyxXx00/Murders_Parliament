using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialManager : MonoBehaviour {

    public GameObject optionsPanel;
    public void Jugar() {
        SceneManager.LoadScene("Nivel_1"); // Carga la escena jugable
    }

    public void Salir() {
        Application.Quit(); // Sale del juego
    }
}
