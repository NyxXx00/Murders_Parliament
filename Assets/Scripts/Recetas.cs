using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe Data")]
public class Recetas : ScriptableObject {
    [Header("Identificación")]
    // Los IDs de los ítems de entrada requeridos
    public string[] RequiredInputIDs;

    [Header("Resultado")]
    //item que se produce al craftear con éxito
    public ItemData ResultItem;

    // Cantidad del resultado
    public int ResultQuantity = 1;
}