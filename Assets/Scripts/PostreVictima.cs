using UnityEngine;

public class PostreVictima : MonoBehaviour {
    [Header("Condiciones del Puzzle")]
    // Asignar en Inspector: El AudioSource de la Destiladora
    public AudioSource destiladoraNoise;

    private bool isPoisoned = false;

    public void OnClick() // Llamada al interactuar con el postre
    {
        if (isPoisoned) {
            Debug.Log("El postre ya está listo.");
            return;
        }

        ItemData selectedItem = Inventario.Instance.DatosItemSeleccionado;

        // Verificar el ítem crafteado (Pintalabios + Veneno)
        if (selectedItem != null && selectedItem.ItemID == "PintalabiosJeringuilla") {
            // Verificar la condición de sincronización
            if (destiladoraNoise != null && destiladoraNoise.isPlaying) {
                // ÉXITO
                ExecuteInjectionSuccess();
            }
            else {
                // FALLO
                Debug.Log("¡Silencio! Necesitas que la destiladora esté en marcha para cubrir el sonido.");
            }
        }
        else {
            Debug.Log("No tengo nada que hacer con este postre.");
        }
    }

    private void ExecuteInjectionSuccess() {
        isPoisoned = true;

        // Consumir el objeto crafteado
        Inventario.Instance.RemoveItem("PintalabiosJeringuilla");

        Debug.Log("¡Éxito! Postre envenenado.");
    }
}
