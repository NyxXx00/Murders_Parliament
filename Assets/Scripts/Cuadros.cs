using UnityEngine;
using UnityEngine.EventSystems;

public class Cuadros : MonoBehaviour {
    [Header("Configuración")]
    public ItemData cuadroData;
    public bool estaVacio;
    public float distanciaMaxima = 3f;

    [Header("Cursores")]
    public Texture2D cursorInteractuar;

    private SpriteRenderer render;
    private Color colorOriginal;
    private Transform jugador;

    void Awake() {
        render = GetComponent<SpriteRenderer>();
        colorOriginal = render.color;
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null) jugador = p.transform;
    }

    void Start() {
        ActualizarVisual();
    }

    private void OnMouseEnter() {
        if (cursorInteractuar != null) Cursor.SetCursor(cursorInteractuar, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit() {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDown() {

        if (DatosCuadros.Instance != null && DatosCuadros.Instance.puzzleCompletado) {
            Debug.Log("El puzzle ya está resuelto, no puedes mover los cuadros.");
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject()) return;

        float dist = Vector2.Distance(transform.position, jugador.position);
        if (dist > distanciaMaxima) return;

        if (!estaVacio) {
            if (Inventario.Instance.AddItem(cuadroData)) {
                cuadroData = null;
                estaVacio = true;
                FinalizarAccion();
            }
        }
        else {
            ItemData seleccionado = Inventario.Instance.DatosItemSeleccionado;
            if (seleccionado != null) {
                cuadroData = seleccionado;
                estaVacio = false;
                Inventario.Instance.RemoveItem(seleccionado.ItemID);
                FinalizarAccion();
            }
        }
    }

    void FinalizarAccion() {
        ActualizarVisual();
        PuzzleManager.Instance.GuardarEstado(); // Guardamos el cambio para la persistencia
        PuzzleManager.Instance.ComprobarOrden();
    }

    public void ActualizarVisual() {
        if (render == null) render = GetComponent<SpriteRenderer>();

        if (estaVacio) {
            render.sprite = null;
            render.color = new Color(1, 1, 1, 0.3f);
        }
        else if (cuadroData != null) {
            render.sprite = cuadroData.icon;
            render.color = colorOriginal;
        }
    }
}