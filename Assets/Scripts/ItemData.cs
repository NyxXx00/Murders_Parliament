using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject {
    public string ItemID;
    public string ItemName;

    [Header("Estado Básico")]
    public string Description;
    public Sprite icon;

    [Header("Configuración del Cursor")]
    public Texture2D cursorTexture;

    [Header("Mecánica de Inspección")]
    public bool tieneSecreto = false;

    public bool inspeccionado = false;

    // secreto
    public ItemData RevealedItemData;

    // texto que se muestra antes de la inspección.
    [TextArea(3, 5)]
    public string SecretPrompt = "Hay algo raro con este objeto...";

    //descripción que se muestra despues de la inspección.
    [TextArea(3, 5)]
    public string RevealedDescription;

    public void OnEnable() {
        // Asegurarse de que el estado de inspección se restablezca al cargar el juego o al reiniciar la escena
        inspeccionado = false;
    }
}