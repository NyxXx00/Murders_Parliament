using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class CraftSlot : MonoBehaviour, IPointerClickHandler {
    // Referencia al CraftPanel para notificarle que revise la receta
    private CraftPanel craftPanel;

    [Header("Componentes del Slot")]
    public Image itemIcon;

    [HideInInspector]
    public ItemData currentItem; // El ítem depositado en ESTE slot

    void Awake() {
        // Búsqueda automática: Asegúrate de que tu CraftPanel esté en la escena
        craftPanel = Object.FindFirstObjectByType<CraftPanel>();
        if (craftPanel == null) {
            Debug.LogError("CraftingSlot: ¡No se encontró el CraftPanel! Necesitas uno en la escena.");
            return;
        }

        // Búsqueda de la Image del icono (asumiendo que es hija)
        if (itemIcon == null) itemIcon = GetComponentInChildren<Image>();

        if (itemIcon != null) itemIcon.enabled = false;
    }

    // Se llama cuando el usuario hace clic en este slot de crafteo (Depositar/Retirar)
    public void OnPointerClick(PointerEventData eventData) {
        // 1. Si el slot tiene un ítem y el usuario hace clic -> RETIRAR
        if (currentItem != null) {
            ReturnItemToInventory();
            return;
        }

        // 2. Si el slot está vacío, intenta DEPOSITAR el ítem seleccionado

        // Obtener el ítem que el jugador tiene activo/seleccionado
        ItemData selectedItem = Inventario.Instance.DatosItemSeleccionado;

        if (selectedItem != null) {
            // CRÍTICO: Mover el ítem del cursor a este slot

            // Eliminar el ítem del inventario (esto deselecciona el cursor y llama a VolverALupa)
            if (!Inventario.Instance.RemoveItem(selectedItem.ItemID)) {
                // Esto no debería suceder si SelectedItemData no es null
                Debug.LogWarning("No se pudo remover el ítem del inventario.");
                return;
            }

            // Depositar el ítem en este slot de crafteo
            currentItem = selectedItem;

            // Actualizar la UI
            if (itemIcon != null) {
                itemIcon.enabled = true;
                itemIcon.sprite = currentItem.icon;
            }

            Debug.Log($"Ítem {currentItem.ItemName} depositado en este slot de crafteo.");

            // Notificar al panel principal para revisar la receta
            craftPanel.CheckForRecipe();
        }
    }

    // Devuelve el ítem de este slot de vuelta al inventario
    private void ReturnItemToInventory() {
        Inventario.Instance.AddItem(currentItem); // Asumimos AddItem siempre funciona
        ClearSlot();
        craftPanel.CheckForRecipe(); // Revisar si la receta se rompió al quitar el ítem
    }

    // Limpia el estado interno y la UI de este slot
    public void ClearSlot() {
        currentItem = null;
        if (itemIcon != null) itemIcon.enabled = false;
    }
}