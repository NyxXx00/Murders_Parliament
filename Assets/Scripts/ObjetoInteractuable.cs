using UnityEngine;
using UnityEngine.Events; // Necesario para UnityEvent

public class ObjetoInteractuable : MonoBehaviour {

    // 1. Estructura que define qué se necesita y qué ocurre al combinar
    [System.Serializable]
    public struct RecetaInteraccion {
        [Tooltip("ID del ítem requerido del inventario")]
        public string ItemIDRequerido;

        [Tooltip("Mensaje a mostrar al jugador si la interacción es exitosa.")]
        [TextArea(1, 3)]
        public string MensajeExito;

        [Tooltip("Si es verdadero, el ítem se consume del inventario al usarlo.")]
        public bool ConsumirItem;

        [Tooltip("Acciones que se ejecutan al completar la interacción")]
        public UnityEvent EventoExito;
    }

    [Header("Configuración de Interacción")]
    [Tooltip("Lista de todas las combinaciones válidas para este objeto.")]
    public RecetaInteraccion[] Recetas;

    [Tooltip("Mensaje a mostrar si se usa un ítem incorrecto o si el objeto ya se usó.")]
    [TextArea(1, 3)]
    public string mensajeFallo = "Ese objeto no funciona aquí.";

    [Tooltip("Si es verdadero, la interacción solo se puede ejecutar una vez.")]
    public bool esUsoUnico = true;

    private bool haSidoUsado = false;

    // Este método es llamado por el Raycast cuando el jugador hace clic con un ítem seleccionado.
    public void UsarItemEnObjeto(ItemData itemUsado) {
        // Verificar uso único 
        if (esUsoUnico && haSidoUsado) {
            SistemaMensajes.Instance.MostrarMensaje("El cupcake ya ha sido alterado.", 3f);
            return;
        }

        string mensajeAMostrar = mensajeFallo; // Por defecto, es el mensaje de fallo genérico

        foreach (var receta in Recetas) {
            // Comparamos el ID del ítem seleccionado con el ID requerido en la receta
            if (itemUsado.ItemID == receta.ItemIDRequerido) {


                mensajeAMostrar = receta.MensajeExito; // Usar el mensaje de éxito de la receta

                // 1. Ejecutar el evento
                receta.EventoExito.Invoke();

                // 2. Consumir el ítem si está marcado en la receta
                if (receta.ConsumirItem && Inventario.Instance != null) {
                    Inventario.Instance.RemoveItem(itemUsado.ItemID);
                }

                // 3. Deseleccionar el ítem y volver al cursor base
                if (Inventario.Instance != null) {
                    Inventario.Instance.DeselectItem();
                }

                haSidoUsado = true;
                break; // Detenemos la búsqueda de recetas
            }
        }

        // 4. Mostrar el mensaje final (éxito o fallo).
        if (SistemaMensajes.Instance != null) {
            SistemaMensajes.Instance.MostrarMensaje(mensajeAMostrar, 3f);
        }
    }
}