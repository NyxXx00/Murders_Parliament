using UnityEngine;

public class InteraccionNPC : MonoBehaviour {
    [Header("Referencias")]
    public GameObject iconoTecla; // El objeto visual encima de la cabeza
    public DialogueTrigger scriptDialogo; // Tu script de diálogo existente

    [Header("Ajustes")]
    public KeyCode teclaInteraccion = KeyCode.Space;
    public float radioDeteccion = 1f;

    private bool jugadorCerca = false;
    private Transform jugador;

    void Start() {
        jugador = GameObject.FindWithTag("Player").transform;
        if (iconoTecla != null) iconoTecla.SetActive(false);
    }

    void Update() {
        float distancia = Vector2.Distance(transform.position, jugador.position);

        // Controlar si el jugador entra o sale del rango
        if (distancia <= radioDeteccion) {
            if (!jugadorCerca) EntrarEnRango();

            // Si está cerca y pulsa la tecla...
            if (Input.GetKeyDown(teclaInteraccion)) {
                ActivarDialogo();
            }
        }
        else {
            if (jugadorCerca) SalirDeRango();
        }
    }

    void EntrarEnRango() {
        jugadorCerca = true;
        if (iconoTecla != null) iconoTecla.SetActive(true);
    }

    void SalirDeRango() {
        jugadorCerca = false;
        if (iconoTecla != null) iconoTecla.SetActive(false);
    }

    void ActivarDialogo() {
        if (scriptDialogo != null) {
            // Ocultamos el icono mientras hablamos
            if (iconoTecla != null) iconoTecla.SetActive(false);

            // Llamamos a la función que inicia el diálogo
            scriptDialogo.TriggerDialogue();
        }
    }
}