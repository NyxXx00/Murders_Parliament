using UnityEngine;
using UnityEngine.EventSystems;

public class SlotHandler : MonoBehaviour, IPointerClickHandler {

    private Inventario inventario;
    private int index;

    public void SetInventory(Inventario inv) {
        inventario = inv;
        index = transform.GetSiblingIndex();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (inventario == null || index >= inventario.slots.Count) return;

        var slot = inventario.slots[index];
        if (slot.item == null) return;

        Item item = slot.item;

        if (InventarioCursor.Instance.GetItemSeleccionado() == item) {
            InventarioCursor.Instance.VolverALupa();
        }
        else {
            InventarioCursor.Instance.SeleccionarItem(item); 
        }
    }
}
