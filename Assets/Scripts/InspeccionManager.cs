using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspeccionManager : MonoBehaviour {

    public static InspeccionManager Instance { get; private set; }

    [Header("Configuración de UI")]
    public GameObject inspectionPanel;
    public Image itemIconDisplay;
    public TextMeshProUGUI itemNameDisplay;
    public TextMeshProUGUI itemDescriptionDisplay;

    private ItemData currentInspectedItem;
    private bool secretHasBeenFound = false;

    private Coroutine closeTimerCoroutine;

    private bool shouldBeActive = false;

    private void Awake() {
        // Singleton
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        inspectionPanel.SetActive(false);
    }

    void LateUpdate() {
        // Forzar la activación después de que todos los cálculos de UI terminen
        if (shouldBeActive && !inspectionPanel.activeSelf) {
            inspectionPanel.SetActive(true);
            Debug.Log("InspeccionManager: ¡Activación forzada en LateUpdate!");
        }
    }

    public void InspectItem(ItemData itemToInspect) {
        currentInspectedItem = itemToInspect;

        inspectionPanel.SetActive(true);
  
        UpdateUI();

        shouldBeActive = true;

        if (closeTimerCoroutine != null) {
            StopCoroutine(closeTimerCoroutine);
        }
        // Inicia la corutina, pasando 5 segundos como duración
        closeTimerCoroutine = StartCoroutine(TimerToClosePanel(3f));

    }

    private void UpdateUI() {
        itemIconDisplay.sprite = currentInspectedItem.icon;
        itemNameDisplay.text = currentInspectedItem.ItemName;

        // Mostrar el texto revelado si el secreto ya se encontró, o si no hay secreto.
        if (secretHasBeenFound || !currentInspectedItem.tieneSecreto) {
            itemDescriptionDisplay.text = currentInspectedItem.RevealedDescription;
        }
        // Mostrar el texto de "sospecha" si hay un secreto pendiente de descubrir.
        else {
            itemDescriptionDisplay.text = currentInspectedItem.SecretPrompt;
        }
    }


    public void AttemptRevealSecret() {
        // asegura de que el ítem tiene un secreto y que no ha sido revelado previamente
        if (currentInspectedItem != null && currentInspectedItem.tieneSecreto && !secretHasBeenFound) {

            //marcar el ítem como inspeccionado en el inventario para guardar el estado
            secretHasBeenFound = true; // Actualiza el estado local

            //añadir el ítem revelado al inventario si existe
            if (currentInspectedItem.RevealedItemData != null) {
                Inventario.Instance.AddItem(currentInspectedItem.RevealedItemData);

                Debug.Log($"Secreto revelado. Ítem añadido: {currentInspectedItem.RevealedItemData.ItemName}");
            }

            // actualizar la UI para reflejar el cambio
            UpdateUI();

        }
    }

    public void StopInspection() {
        inspectionPanel.SetActive(false);

        shouldBeActive = false;

        // reactivar el inventario si estaba abierto antes de la inspección
        if (Inventario.Instance != null) {


            //reactiva los componentes del Inventario
            Inventario.Instance.panelInventario.gameObject.SetActive(true);
            Inventario.Instance.contenedorItems.gameObject.SetActive(true);

            // reactiva el panel de crafteo si es parte del inventario
            if (Inventario.Instance.panelCrafteo != null) {
                Inventario.Instance.panelCrafteo.SetActive(true);
            }
        }
    }

    private System.Collections.IEnumerator TimerToClosePanel(float duration) {
        Debug.Log($"El panel de inspección se cerrará automáticamente en {duration} segundos.");

        // Espera el tiempo especificado (5 segundos)
        yield return new WaitForSeconds(duration);

        // Después de esperar, llama a la función de cierre
        StopInspection();
    }
}
