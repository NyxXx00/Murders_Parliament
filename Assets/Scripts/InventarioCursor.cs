using UnityEngine;
using UnityEngine.InputSystem;

public class InventarioCursor : MonoBehaviour {

    public static InventarioCursor Instance { get; private set; }

    [Header("Configuración")]
    // Textura de cursor predeterminada 
    public Texture2D texturaCursorBase;

    // Ajusta el hotspot según el tamaño final de tu cursor
    public Vector2 hotspot = new Vector2(32, 32);

    private ItemData itemSeleccionado = null; // Variable en español

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    void Start() {
        // Inicializa con la textura base, si está asignada
        VolverACursorBase();

    }

    // Establece el ítem en el cursor y cambia la textura.
    public void SeleccionarItem(ItemData item) {
        itemSeleccionado = item;

        if (item != null && item.cursorTexture != null) {
            // Usa la textura específica del ítem
            Cursor.SetCursor(item.cursorTexture, hotspot, CursorMode.Auto);
        }
        else {
            // Si el ítem es nulo o no tiene textura, vuelve al cursor base
            VolverACursorBase();
        }
    }


    //Restablece el cursor al modo base y borra el ítem seleccionado.
    public void VolverACursorBase() {
        itemSeleccionado = null;

        // Si hay una textura base asignada, úsala. Si no, usa el cursor por defecto del sistema (null).
        Cursor.SetCursor(texturaCursorBase, Vector2.zero, CursorMode.Auto);
    }

    // Devuelve el ItemData que actualmente está siendo "sostenido" por el cursor.
    public ItemData GetItemSeleccionado() => itemSeleccionado;

    void Update() {
        // Lógica para soltar el ítem con el clic derecho
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame) {
            if (itemSeleccionado != null) {
                VolverACursorBase(); // Llama al método renombrado

                // Asegura que la ranura del inventario se deseleccione también
                if (Inventario.Instance != null) {
                    Inventario.Instance.DeselectItem();
                }
            }
        }
    }
}