using UnityEngine;

public class PickupItem : MonoBehaviour {

    public ItemData itemData;
    public int quantity = 1;

    // Bandera para saber si el jugador está en rango de clic
    private bool canBePickedUp = false;

    // ID de la tarea que se marcará como completada al recibir el objeto
    public string taskID; 

    // Referencia al inventario
    private Inventario inventario;

    void Start() {
        // Asegúrate de obtener la instancia del inventario al inicio
        inventario = Inventario.Instance;
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

            //Intenta añadir el objeto
            if (inventario.AddItem(itemData, quantity)) {

                Debug.Log("Objeto recogido con Clic: " + itemData.ItemName);

                // COMPLETAR LA TAREA
                if (TareasManager.Instance != null && taskID != "") {
                    TareasManager.Instance.CompleteTask(taskID);
                }

                //destruye el objeto del mundo
                canBePickedUp = false;
                Destroy(gameObject);
            }
            else {
                Debug.Log("Inventario lleno.");
            }
        }
    }
}