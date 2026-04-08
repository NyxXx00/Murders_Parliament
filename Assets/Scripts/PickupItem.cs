using UnityEngine;

public class PickupItem : MonoBehaviour {

    public ItemData itemData;
    public int quantity = 1;

    [Header("Persistencia")]
    public string uniqueID;

    // Bandera para saber si el jugador está en rango de clic
    private bool canBePickedUp = false;

    // ID de la tarea que se marcará como completada al recibir el objeto
    public string taskID;

    // Referencia al inventario
    private Inventario inventario;

    void Start() {
        inventario = Inventario.Instance;

        // Si el ID ya está registrado como completado/recogido, eliminamos el objeto
        if (GameManager.Instance != null && GameManager.Instance.IsItemPickedUp(uniqueID)) {
            Destroy(gameObject);
            return;
        }
    }

    void Update() {
        //Si el jugador está en rango Y presiona el botón izquierdo del ratón
        if (canBePickedUp && Input.GetMouseButtonDown(0)) {
            TryPickup();
        }
    }

    // El jugador entra en el área de recolección
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canBePickedUp = true;
            Debug.Log("Objeto listo para ser recogido con clic.");
        }
    }

    // El jugador sale del área de recolección
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canBePickedUp = false;
        }
    }

    private void TryPickup() {
        if (inventario != null) {
            if (inventario.AddItem(itemData, quantity)) {

                Debug.Log("Objeto recogido: " + itemData.ItemName);

                if (GameManager.Instance != null && !string.IsNullOrEmpty(uniqueID)) {
                    GameManager.Instance.RegisterPickup(uniqueID);
                }

                if (itemData.ItemName == "ID_Aceitera") {
                    GameManager.Instance.tieneAceite = true;
                }

                if (TareasManager.Instance != null && !string.IsNullOrEmpty(taskID)) {
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