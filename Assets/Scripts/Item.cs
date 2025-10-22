using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Item", menuName = "Inventario/Item")]
public class Item : ScriptableObject {
    public string itemName = "Nuevo Item";
    public Sprite icon = null;

    public virtual void Use() {
        Debug.Log("Usando " + itemName);
    }
}
