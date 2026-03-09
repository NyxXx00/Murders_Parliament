using UnityEngine;

public class DoctoraItem : MonoBehaviour
{
    public ItemData itemToGive;   // El objeto que te dará
    public int quantity = 1;      // cantidad
<<<<<<< HEAD

    public string taskID; // ID de la tarea que se marcará como completada al recibir el objeto
=======
>>>>>>> 998451dad077bfbe3afa4da851f744ad535d8fa7
    private Inventario inventario;

    void Start() {
        inventario = Inventario.Instance;
    }

    void OnMouseDown()   // Detecta clic sobre el NPC
    {
        if (inventario != null) {
            if (inventario.AddItem(itemToGive, quantity)) {
                Debug.Log("NPC te dio: " + itemToGive.ItemName);
<<<<<<< HEAD

                // COMPLETAR LA TAREA
                if (TareasManager.Instance != null && taskID != "") {
                    TareasManager.Instance.CompleteTask(taskID);
                }

=======
>>>>>>> 998451dad077bfbe3afa4da851f744ad535d8fa7
                //para que te lo dé solo una vez
                Destroy(this);
            }
            else {
                Debug.Log("Inventario lleno, no puedes recibir el objeto.");
            }
        }
    }
}
