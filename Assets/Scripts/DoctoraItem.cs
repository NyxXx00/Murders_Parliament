using UnityEngine;

public class DoctoraItem : MonoBehaviour
{
    public ItemData itemToGive;   // El objeto que te dará
    public int quantity = 1;      // cantidad
    private Inventario inventario;

    void Start() {
        inventario = Inventario.Instance;
    }

    void OnMouseDown()   // Detecta clic sobre el NPC
    {
        if (inventario != null) {
            if (inventario.AddItem(itemToGive, quantity)) {
                Debug.Log("NPC te dio: " + itemToGive.ItemName);
                // OPCIONAL: para que te lo dé solo una vez
                // Destroy(this); // elimina el script para que no repita entrega
            }
            else {
                Debug.Log("Inventario lleno, no puedes recibir el objeto.");
            }
        }
    }
}
