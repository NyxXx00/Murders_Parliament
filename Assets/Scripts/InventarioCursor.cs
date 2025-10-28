using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using static UnityEditor.PlayerSettings;

public class InventarioCursor : MonoBehaviour {

    public static InventarioCursor Instance { get; private set; }

    [Header("Configuración")]
    public Vector2 hotspot = new Vector2(16, 16);

    private Item itemSeleccionado = null;

    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start() {
        // Usa el cursor por defecto de Project Settings
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void SeleccionarItem(Item item) {
        itemSeleccionado = item;
        if (item != null && item.cursorIcon != null) {
            Cursor.SetCursor(item.cursorIcon, hotspot, CursorMode.Auto);
        }
        else {
            VolverALupa();
        }
    }

    public void VolverALupa() {
        itemSeleccionado = null;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // ← Vuelve a Project Settings
    }

    public Item GetItemSeleccionado() => itemSeleccionado;

    void Update() {
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame) {
            VolverALupa();
        }
    }
}
