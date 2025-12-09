using UnityEngine;
using TMPro;
using System.Collections;

public class SistemaMensajes : MonoBehaviour {

    // Singleton
    public static SistemaMensajes Instance { get; private set; }

    [Header("Referencias de UI")]
    public GameObject panelMensaje;
    public TMP_Text textoMensaje;

    private void Awake() {
        // Singleton
        if (Instance == null) {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            // Asegura que el panel esté oculto al inicio
            if (panelMensaje != null) {
                panelMensaje.SetActive(false); 
            }
        }
        else {
            Destroy(gameObject);
        }
    }

    // Muestra un mensaje temporal en pantalla.
    public void MostrarMensaje(string mensaje, float duracion = 4.0f) {
        if (panelMensaje == null || textoMensaje == null) {
            Debug.LogError("Referencias de UI no asignadas en SistemaMensajes.");
            return;
        }

        // Detiene cualquier corrutina de mensaje anterior
        StopAllCoroutines();

        textoMensaje.text = mensaje;
        panelMensaje.SetActive(true);

        // Inicia la corrutina para ocultar el mensaje después de la duración
        StartCoroutine(OcultarMensajeDespuesDe(duracion));
    }

    private IEnumerator OcultarMensajeDespuesDe(float tiempo) {
        yield return new WaitForSeconds(tiempo);
        if (panelMensaje != null) {
            panelMensaje.SetActive(false);
        }
    }
}