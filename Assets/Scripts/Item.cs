using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Item", menuName = "Inventario/Item")]
public class Item : ScriptableObject {

    public string itemName;
    public Sprite icon;          
    public Texture2D cursorIcon;
    public int maxStackSize = 99;
    public bool esConsumible = true;
}
