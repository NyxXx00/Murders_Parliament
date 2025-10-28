using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaInteractiva : MonoBehaviour {

    [Header("Configuración")]
    public Item llaveRequerida;
    public string nombreEscenaSiguiente = "Nivel_1_Despacho"; 
    public float tiempoTransicion = 1f;

    private bool abierta = false;

    void OnMouseDown() {
        if (abierta) return;

        Item item = InventarioCursor.Instance.GetItemSeleccionado();

        if (item != null && item == llaveRequerida) {
            AbrirPuerta();
            if (llaveRequerida.esConsumible) {
                Inventario.Instance.AddItem(llaveRequerida, -1);
            }
            InventarioCursor.Instance.VolverALupa();
        }
        else {
            Debug.Log("¡Llave incorrecta o no seleccionada!");
        }
    }

    void AbrirPuerta() {
        abierta = true;
        Debug.Log("¡Puerta abierta! CARGANDO: " + nombreEscenaSiguiente);

        // Carga la siguiente escena
        SceneManager.LoadScene(nombreEscenaSiguiente);
    }
}