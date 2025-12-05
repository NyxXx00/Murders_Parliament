using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CraftPanel : MonoBehaviour {

    public CraftSlot[] inputSlots;
    public Image outputIcon;
    public Button craftButton;
    private ItemData currentRecipeResult;

    // lista de todas las recetas cargadas
    private List<Recetas> allRecipes;

    void Awake() {
        // buscar todos los CraftSlots hijos
        inputSlots = GetComponentsInChildren<CraftSlot>();

        // cargar todas las recetas desde Resources/Recipes
        allRecipes = Resources.LoadAll<Recetas>("Recipes").ToList();

        if (allRecipes.Count == 0) {
            Debug.LogError("CraftPanel: No se encontraron recetas en la carpeta Recetas.");
        }

        if (craftButton != null) {
            craftButton.onClick.AddListener(AttemptCraft);
            craftButton.interactable = false;
        }
        if (outputIcon != null) outputIcon.enabled = false;
    }

  

    //Función central llamada por CADA CraftingSlot al depositar/retirar un ítem
    public void CheckForRecipe() {
        currentRecipeResult = null;

        // recopilar IDs de ítems en los slots de entrada
        string[] currentInputIDs = inputSlots
            .Where(slot => slot.currentItem != null)
            .Select(slot => slot.currentItem.ItemID)
            .OrderBy(id => id) // ordenar para comparación
            .ToArray();

        //sin items no hay receta
        if (currentInputIDs.Length == 0) {
            UpdateOutputUI();
            return;
        }

        //verificar cada receta
        foreach (var recipe in allRecipes) {
            // Ordenar los IDs requeridos de la receta también
            string[] requiredIDs = recipe.RequiredInputIDs.OrderBy(id => id).ToArray();

            //comparar IDs
            if (currentInputIDs.SequenceEqual(requiredIDs)) {
                // receta encontrada
                currentRecipeResult = recipe.ResultItem;
                break; // Detener la búsqueda
            }
        }

        // actualizar la UI de salida
        UpdateOutputUI();
    }

    private void UpdateOutputUI() {
        bool recipeFound = currentRecipeResult != null;

        if (outputIcon != null) {
            outputIcon.enabled = recipeFound;
            // Usa el sprite del resultado de la receta
            outputIcon.sprite = currentRecipeResult?.icon;
        }

        if (craftButton != null) {
            craftButton.interactable = recipeFound;
        }
    }

    public void AttemptCraft() {
        if (currentRecipeResult == null) return;

        // añadir el ítem resultante al inventario
        Inventario.Instance.AddItem(currentRecipeResult);

        Debug.Log($"¡Receta exitosa! Creado: {currentRecipeResult.ItemName}");

        //limpiar los slots de entrada
        foreach (var slot in inputSlots) {
            slot.ClearSlot();
        }

        // limpiar el resultado actual y actualizar la UI
        currentRecipeResult = null;
        UpdateOutputUI();
    }
}