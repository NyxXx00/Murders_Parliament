using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialManager : MonoBehaviour {

    public string escena; // Nombre de la escena jugable
    public GameObject optionsPanel;
    public void Jugar() {
        SceneManager.LoadScene(escena); // Carga la escena jugable
    }

    public void Salir() {
        Application.Quit(); // Sale del juego
    }
}
