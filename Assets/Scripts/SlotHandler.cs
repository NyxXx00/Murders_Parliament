using UnityEngine;
using UnityEngine.EventSystems;

public class SlotHandler : MonoBehaviour, IPointerClickHandler {
    private Inventario inventario;
    private int slotIndex;

    // El inventario llama a esta función al crear el slot
    public void SetInventory(Inventario inv, int index) {
        inventario = inv;
        slotIndex = index;
    }

    // llama a esta función cuando se hace clic en el slot
    public void OnPointerClick(PointerEventData eventData) {
        if (inventario != null) {
            // verifica que estés pasando clickCount
            inventario.HandleSlotInteraction(slotIndex, eventData.clickCount);
            Debug.Log("Doble clic detectado en slot: " + slotIndex);
        }
    }
}