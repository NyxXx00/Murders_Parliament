using UnityEngine;

public class PiedraJardin : MonoBehaviour {
    [Header("Configuración Visual")]
    public GameObject piedraVisual;
    public ItemData objetoPiedra;

    [Header("Cursores (Opcional)")]
    public Texture2D cursorInteractuar;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // Raycast para detectar el clic en la zona
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject) {
                ComprobarInventarioYActivar();
            }
        }
    }

    void ComprobarInventarioYActivar() {
        if (Inventario.Instance.DatosItemSeleccionado != null) {

            if (Inventario.Instance.DatosItemSeleccionado == objetoPiedra) {
                LanzamientoExitoso();
            }
            else {
                Debug.Log("Este objeto no sirve aquí, necesitas la piedra.");
            }
        }
        else {
            Debug.Log("No has seleccionado la piedra del inventario.");
        }
    }

    void LanzamientoExitoso() {
        if (piedraVisual != null) piedraVisual.SetActive(true);

        Infiltracion manejador = Object.FindFirstObjectByType<Infiltracion>();
        if (manejador != null) {
            manejador.LanzarPiedra();
            Debug.Log("¡Piedra puesta! Sirvientas en movimiento.");
        }

        Inventario.Instance.RemoveItem(objetoPiedra.ItemID);
        Inventario.Instance.DeselectItem();
    }

    private void OnMouseEnter() {
        if (cursorInteractuar != null) Cursor.SetCursor(cursorInteractuar, Vector2.zero, CursorMode.Auto);
    }
}
