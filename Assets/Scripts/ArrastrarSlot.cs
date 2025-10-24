using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Inventario;
public class ArrastrarSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {
   
    private Inventario inventario;
    private int index;
    private Canvas canvas;
    private GameObject draggedItem;
    private CanvasGroup canvasGroup;

    void Start() {
        inventario = FindObjectOfType<Inventario>();
        index = transform.GetSiblingIndex();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (inventario != null && inventario.slots[index].item != null) {
            // Crea una copia del ítem para arrastrar
            draggedItem = new GameObject("DraggedItem");
            Image draggedImage = draggedItem.AddComponent<Image>();
            draggedImage.sprite = inventario.slots[index].item.icon;
            draggedImage.raycastTarget = false;
            CanvasGroup draggedCG = draggedItem.AddComponent<CanvasGroup>();
            draggedCG.blocksRaycasts = false;

            draggedItem.transform.SetParent(canvas.transform, false);
            draggedItem.transform.localScale = Vector3.one;
            canvasGroup.alpha = 0.5f; // Hace el slot semi-transparente
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (draggedItem != null) {
            draggedItem.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (draggedItem != null) {
            Destroy(draggedItem);
            canvasGroup.alpha = 1f; // Restaura la opacidad
            if (eventData.pointerCurrentRaycast.gameObject == null || !eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotDragHandler>()) {
                // Si se suelta fuera de un slot, elimina el ítem
                inventario.slots[index].item = null;
                inventario.slots[index].quantity = 0;
                inventario.slots[index].UpdateUI();
                Debug.Log("Ítem eliminado del slot " + index);
            }
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null) {
            SlotDragHandler sourceSlot = eventData.pointerDrag.GetComponent<SlotDragHandler>();
            if (sourceSlot != null && sourceSlot != this) {
                int sourceIndex = sourceSlot.index;
                int targetIndex = index;

                // Intercambia ítems entre slots
                InventorySlot sourceSlotData = inventario.slots[sourceIndex];
                InventorySlot targetSlotData = inventario.slots[targetIndex];

                Item tempItem = targetSlotData.item;
                int tempQuantity = targetSlotData.quantity;

                targetSlotData.item = sourceSlotData.item;
                targetSlotData.quantity = sourceSlotData.quantity;

                sourceSlotData.item = tempItem;
                sourceSlotData.quantity = tempQuantity;

                targetSlotData.UpdateUI();
                sourceSlotData.UpdateUI();
                Debug.Log("Ítem intercambiado de slot " + sourceIndex + " a slot " + targetIndex);
            }
        }
    }
}
}
