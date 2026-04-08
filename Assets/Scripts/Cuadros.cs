using UnityEngine;
using UnityEngine.EventSystems;

public class Cuadros : MonoBehaviour {
    [Header("Configuración")]
    public ItemData cuadroData; // El item que tiene este marco actualmente
    public bool estaVacio;      // ¿Hay un cuadro aquí?

    private SpriteRenderer render;
    private Color colorOriginal;

    void Awake() {
        render = GetComponent<SpriteRenderer>();
        colorOriginal = render.color;
        ActualizarVisual();
    }

    private void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // ACCIÓN A: RECOGER (Si el marco tiene un cuadro)
        if (!estaVacio) {
            bool seAgrego = Inventario.Instance.AddItem(cuadroData);
            if (seAgrego) {
                cuadroData = null;
                estaVacio = true;
                ActualizarVisual();
                PuzzleManager.Instance.ComprobarOrden();
            }
        }
        // ACCIÓN B: COLOCAR (Si el marco está vacío y hay algo seleccionado en el inventario)
        else {
            ItemData itemSeleccionado = Inventario.Instance.DatosItemSeleccionado;

            if (itemSeleccionado != null) {
                // Colocamos el item en este hueco
                cuadroData = itemSeleccionado;
                estaVacio = false;

                // Lo eliminamos del inventario usando tu método RemoveItem
                Inventario.Instance.RemoveItem(itemSeleccionado.ItemID);

                ActualizarVisual();
                PuzzleManager.Instance.ComprobarOrden();
            }
        }
    }

    public void ActualizarVisual() {
        if (estaVacio) {
            render.sprite = null;
            // Opcional: hacer el marco semitransparente para que se vea "hueco"
            render.color = new Color(1, 1, 1, 0.3f);
        }
        else {
            render.sprite = cuadroData.icon; // Usamos .icon que es el que tienes en ItemData
            render.color = colorOriginal;
        }
    }
}