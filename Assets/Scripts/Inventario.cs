using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour {

    public Button toggleButton;
    public RectTransform panel;
    public Transform itemsContainer;
    public GameObject circleSlotPrefab;

    public float openHeight = 300f;
    public float animationSpeed = 20f;
    public int slotCount = 6;

    private bool wasOpenLastFrame;
    private float lastHeight;

    private float lastToggleTime = -1f;
    private float toggleCooldown = 0.2f;

    private bool isOpen = false;
    private List<InventorySlot> slots = new List<InventorySlot>();

    public void Start() {
        if (panel == null || toggleButton == null || itemsContainer == null || circleSlotPrefab == null) {
            Debug.LogError("Alguna referencia en Inventario no está asignada.");
            return;
        }
        Debug.Log("Inicializando panel. SizeDelta: " + panel.sizeDelta);
        panel.sizeDelta = new Vector2(panel.sizeDelta.x, 0); // Cerrado
        itemsContainer.gameObject.SetActive(false); // Oculta ItemsContainer al inicio
        toggleButton.onClick.AddListener(Toggle);

        for (int i = 0; i < slotCount; i++) {
            GameObject slot = Instantiate(circleSlotPrefab, itemsContainer);
            Image icon = slot.transform.Find("ItemIcon").GetComponent<Image>();
            TextMeshProUGUI qty = slot.transform.Find("QuantityText")?.GetComponent<TextMeshProUGUI>();
            slots.Add(new InventorySlot(null, 0, icon, qty));
            Debug.Log("Slot creado en ItemsContainer: " + i + ", Parent: " + itemsContainer.name);
        }
    }

    public void Update() {
        // Solo loguea si hay un cambio significativo
        if (isOpen != wasOpenLastFrame || Mathf.Abs(panel.sizeDelta.y - lastHeight) > 0.1f) {
            Debug.Log("Panel height: " + panel.sizeDelta.y + ", isOpen: " + isOpen);
            wasOpenLastFrame = isOpen;
            lastHeight = panel.sizeDelta.y;
        }
        if (isOpen && panel.sizeDelta.y < openHeight) {
            float newHeight = panel.sizeDelta.y + (animationSpeed * Time.deltaTime);
            panel.sizeDelta = new Vector2(panel.sizeDelta.x, Mathf.Min(newHeight, openHeight));
            if (!itemsContainer.gameObject.activeSelf) itemsContainer.gameObject.SetActive(true);
            Debug.Log("Abriendo: " + panel.sizeDelta.y);
        }
        else if (!isOpen && panel.sizeDelta.y > 0) {
            float newHeight = panel.sizeDelta.y - (animationSpeed * Time.deltaTime);
            panel.sizeDelta = new Vector2(panel.sizeDelta.x, Mathf.Max(newHeight, 0));
            Debug.Log("Cerrando: " + panel.sizeDelta.y);
        }
        else if (!isOpen && panel.sizeDelta.y == 0) {
            if (itemsContainer.gameObject.activeSelf) itemsContainer.gameObject.SetActive(false);
        }

        toggleButton.GetComponentInChildren<TextMeshProUGUI>().text = isOpen ? "Close" : "Open";
    }

    public void Toggle() {
        if (Time.time - lastToggleTime < toggleCooldown) return; // Evita toggles rápidos
        isOpen = !isOpen;
        lastToggleTime = Time.time;
        Debug.Log("Toggle ejecutado. isOpen = " + isOpen);
    }

    public bool AddItem(Item item, int quantity = 1) {

        foreach (var slot in slots) {
            if (slot.item == item && slot.quantity < item.maxStackSize) {
                slot.quantity += quantity;
                slot.UpdateUI();
                return true;
            }
        }
        foreach (var slot in slots) {
            if (slot.item == null) {
                slot.item = item;
                slot.quantity = quantity;
                slot.UpdateUI();
                return true;
            }
        }
        return false;
    }

    private void OnSlotClicked(int index) {
        if (index >= 0 && index < slots.Count) {
            InventorySlot slot = slots[index];
            if (slot.item != null) {
                Debug.Log("Clic en slot " + index + ": " + slot.item.itemName + ", Cantidad: " + slot.quantity);
                // Ejemplo: Reducir cantidad o eliminar ítem
                slot.quantity--;
                if (slot.quantity <= 0) {
                    slot.item = null;
                }
                slot.UpdateUI();
            }
            else {
                Debug.Log("Clic en slot " + index + ": Vacío");
            }
        }
    }

    [System.Serializable]
    public class InventorySlot {
        public Item item;
        public int quantity;
        public Image icon;
        public TextMeshProUGUI quantityText;

        public InventorySlot(Item i, int q, Image img, TextMeshProUGUI txt) {
            item = i; quantity = q; icon = img; quantityText = txt;
            UpdateUI();
        }

        public void UpdateUI() {
            if (icon != null) {
                icon.enabled = item != null;
                icon.sprite = item?.icon;
            }
            if (quantityText != null) {
                quantityText.enabled = item != null && quantity > 1;
                quantityText.text = quantity > 1 ? quantity.ToString() : "";
            }
        }
    }
}
