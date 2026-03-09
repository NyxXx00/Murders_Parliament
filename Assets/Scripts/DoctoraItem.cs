using UnityEngine;

public class DoctoraItem : MonoBehaviour
{
    public ItemData itemToGive;   // El objeto que te dará
    public int quantity = 1;      // cantidad

    public string taskID; // ID de la tarea que se marcará como completada al recibir el objeto
    private Inventario inventario;

    void Start() {
        inventario = Inventario.Instance;
    }

    void OnMouseDown()   // Detecta clic sobre el NPC
    {
        if (inventario != null) {
            if (inventario.AddItem(itemToGive, quantity)) {
                Debug.Log("NPC te dio: " + itemToGive.ItemName);

                // COMPLETAR LA TAREA
                if (TareasManager.Instance != null && taskID != "") {
                    TareasManager.Instance.CompleteTask(taskID);
                }

                //para que te lo dé solo una vez
                Destroy(this);
            }
            else {
                Debug.Log("Inventario lleno, no puedes recibir el objeto.");
            }
        }
    }
}
