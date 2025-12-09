using UnityEngine;

public class CambioCupcake : MonoBehaviour {


    [Header("ID de item necesario para interactuar")]
    public string itemNecesario; 

    [Header("Cambio color")]
    public Color colorCorrecto = Color.green;

    private SpriteRenderer sr;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() {
        // Si no hay item seleccionado, ignora
        if (Inventario.Instance.RanuraSeleccionada == null) {
            Debug.Log("No hay item seleccionado");
            return;
        }

        // Obtenemos el item que el jugador tiene seleccionado
        ItemData item = Inventario.Instance.DatosItemSeleccionado;

        if (item != null && item.ItemID == itemNecesario) {
            // objeto correcto = cambia color
            sr.color = colorCorrecto;
            Debug.Log(" Interacción correcta con " + itemNecesario);

            // elimina item si se desea
            Inventario.Instance.RemoveItem(itemNecesario);
        }
        else {
            Debug.Log(" Este objeto no sirve para interactuar");
        }
    }
}
