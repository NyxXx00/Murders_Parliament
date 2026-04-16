using UnityEngine;

public class PickupItem : MonoBehaviour {

    public ItemData itemData;
    public int quantity = 1;

    [Header("Persistencia")]
    public string uniqueID;

    // Bandera para saber si el jugador está en rango de clic
    private bool canBePickedUp = false;

    [Header("Tarea Asociada")]
    // CAMBIO: Ahora usamos int para que coincida con el nuevo TareasManager
    public int taskID;

    private Inventario inventario;

    void Start() {
        inventario = Inventario.Instance;

        if (GameManager.Instance != null && GameManager.Instance.IsItemPickedUp(uniqueID)) {
            Destroy(gameObject);
            return;
        }
    }

    void Update() {
        if (canBePickedUp && Input.GetMouseButtonDown(0)) {
            TryPickup();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canBePickedUp = true;
            Debug.Log("Objeto listo: " + itemData.ItemName);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canBePickedUp = false;
        }
    }

    private void TryPickup() {
        if (inventario != null) {
            if (inventario.AddItem(itemData, quantity)) {

                Debug.Log("Objeto recogido: " + itemData.ItemName);

                // Persistencia en el GameManager
                if (GameManager.Instance != null && !string.IsNullOrEmpty(uniqueID)) {
                    GameManager.Instance.RegisterPickup(uniqueID);
                }

                // Lógica específica para el aceite
                if (itemData.ItemName == "ID_Aceitera") {
                    GameManager.Instance.tieneAceite = true;
                }

                // LLAMADA AL NUEVO TAREAS MANAGER
                // Si taskID es 0, podemos asumir que este objeto no completa ninguna tarea
                if (TareasManager.Instance != null && taskID != 0) {
                    TareasManager.Instance.CompleteTask(taskID);
                }

                canBePickedUp = false;
                Destroy(gameObject);
            }
            else {
                Debug.Log("Inventario lleno.");
            }
        }
    }
}