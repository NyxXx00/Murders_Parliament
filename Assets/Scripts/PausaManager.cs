using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaManager : MonoBehaviour {

    [SerializeField] private GameObject panelPausa; // Referencia al panel del menú de pausa
    [SerializeField] private InputSystem_Actions inputActions; // Referencia al Input Actions
    private bool estaPausado = false;

    private void Awake() {
        // Inicializa el Input System
        if (inputActions != null) {
            inputActions.Player.Enable(); // Ajusta "Player" al nombre de tu Action Map
        }
        else {
            Debug.LogError("InputSystem_Actions no está asignado en el Inspector.");
        }
    }

    private void OnEnable() {
        // Escucha la acción "Pause" si inputActions está asignado
        if (inputActions != null) {
            inputActions.Player.Pause.performed += ctx => TogglePausa(); // Ajusta "Pause" al nombre de tu acción
        }
    }

    private void OnDisable() {
        // Desactiva la escucha cuando el script se deshabilita
        if (inputActions != null) {
            inputActions.Player.Pause.performed -= ctx => TogglePausa();
        }
    }

    void Start() {
        // Desactiva el panel de pausa al inicio
        if (panelPausa != null) {
            panelPausa.SetActive(false);
            Debug.Log("Panel de pausa asignado y desactivado al inicio.");
        }
        else {
            Debug.LogError("El panel de pausa no está asignado en el Inspector.");
        }
    }

    private void TogglePausa() {
        if (estaPausado) {
            ReanudarJuego();
        }
        else {
            PausarJuego();
        }
    }

    public void PausarJuego() {
        if (panelPausa != null) {
            panelPausa.SetActive(true);
            Time.timeScale = 0f; // Pausa el juego
            estaPausado = true;
            Debug.Log("Juego pausado.");
        }
    }

    public void ReanudarJuego() {
        if (panelPausa != null) {
            panelPausa.SetActive(false);
            Time.timeScale = 1f; // Reanuda el juego
            estaPausado = false;
            Debug.Log("Juego reanudado.");
        }
    }

    public void SalirAlMenuInicial() {
        Time.timeScale = 1f; // Reanuda el tiempo antes de cambiar de escena
        SceneManager.LoadScene("MenuInicial"); // Carga la escena del menú inicial
    }
}
