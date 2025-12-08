using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;

public class InventarioCursor : MonoBehaviour {

    public static InventarioCursor Instance { get; private set; }

    [Header("Configuración")]
    public Texture2D texturaCursorBase;
    public Vector2 hotspot = new Vector2(32, 32);

    private ItemData itemSeleccionado = null;

    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start() {
        // Al inicio, establecemos el cursor base.
        VolverACursorBase();
    }

    public void SeleccionarItem(ItemData item) {
        itemSeleccionado = item;

        // Cargar la textura del ítem o volver a la base.
        if (item != null && item.cursorTexture != null) {
            Cursor.SetCursor(item.cursorTexture, hotspot, CursorMode.Auto);
        }
        else {
            VolverACursorBase();
        }
    }

    // Esta función ahora será llamada SÓLO por Inventario.DeselectItem() o por ObjetoInteractuable.
    public void VolverACursorBase() {
        itemSeleccionado = null;

        // Aseguramos que el cursor base se establezca con su propio hotspot
        Cursor.SetCursor(texturaCursorBase, hotspot, CursorMode.Auto);
    }

    public ItemData GetItemSeleccionado() => itemSeleccionado;

    void Update() {
        // Si haces clic DERECHO, siempre deselecciona.
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame) {
            if (itemSeleccionado != null) {

                if (Inventario.Instance != null) {
                    Inventario.Instance.DeselectItem();

                }
            }
        }

    }
}