using UnityEngine;

public class DisparadorPuzzle : MonoBehaviour {
    [Header("Referencias")]
    public GameObject panelPuzzle;
    public float distanciaInteraccion = 3f;

    [Header("Objetos a Desaparecer")]
    public GameObject spriteTapa;
    public GameObject cuadroTorcido;


    [Header("Cursores")]
    public Texture2D cursorLupa;

    private Transform jugador;
    private bool puzzleCompletado = false;

    void Start() {
        jugador = GameObject.FindWithTag("Player").transform;
    }

    // Cambiar el cursor al pasar por encima
    void OnMouseEnter() {
        if (!puzzleCompletado && cursorLupa != null) {
            Cursor.SetCursor(cursorLupa, Vector2.zero, CursorMode.Auto);
        }
    }

    void OnMouseExit() {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // Al hacer clic, comprobamos distancia y abrimos el panel
    void OnMouseDown() {

        if (puzzleCompletado) return;

        Debug.Log("1. Clic detectado");

        if (jugador == null) {
            Debug.LogError("¡OJO! No encuentro al jugador. Asegúrate de que Elvira tenga el Tag 'Player'");
            return;
        }

        float dist = Vector2.Distance(transform.position, jugador.position);
        Debug.Log("2. Distancia actual: " + dist + " | Distancia necesaria: " + distanciaInteraccion);

        if (dist <= distanciaInteraccion) {
            Debug.Log("3. Distancia correcta. Intentando activar panel...");
            if (panelPuzzle != null) {
                panelPuzzle.SetActive(true);
                Debug.Log("4. ¡Panel activado!");
            }
            else {
                Debug.LogError("¡ERROR! No has arrastrado el Panel al script en el Inspector");
            }
        }
        else {
            Debug.Log("5. Demasiado lejos.");
        }
    }

    void AbrirPuzzle() {
        if (panelPuzzle != null) {
            panelPuzzle.SetActive(true);

            // --- AÑADE ESTO ---
            // Desactivamos el collider para que no detecte el ratón mientras el puzzle está abierto
            if (GetComponent<Collider2D>() != null) {
                GetComponent<Collider2D>().enabled = false;
            }
            // Resetemos el cursor para que no se quede la lupa puesta
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public void FinalizarPuzzleYLimpiarEscena() {
        puzzleCompletado = true;

        // Desactivamos los objetos visuales viejos
        if (spriteTapa != null) spriteTapa.SetActive(false);
        if (cuadroTorcido != null) cuadroTorcido.SetActive(false);

        // Cerramos el panel del puzzle
        if (panelPuzzle != null) panelPuzzle.SetActive(false);

        // Desactivamos este disparador para siempre
        this.enabled = false;
        if (GetComponent<Collider2D>() != null) GetComponent<Collider2D>().enabled = false;

        Debug.Log("Puzzle completado: Objetos antiguos eliminados.");
    }


    public void ReactivarInteraccion() {
        if (GetComponent<Collider2D>() != null) {
            GetComponent<Collider2D>().enabled = true;
        }
    }

    public void CerrarPanel() {
        if (panelPuzzle != null) {
            panelPuzzle.SetActive(false);
            ReactivarInteraccion();
        }
    }
}