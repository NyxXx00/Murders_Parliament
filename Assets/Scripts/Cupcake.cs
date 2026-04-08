using UnityEngine;

public class Cupcake : MonoBehaviour {

    // ID de la tarea que se marcará como completada al recibir el objeto
    public string taskID;

    [Header("Configuración")]
    public ItemData objetoNeceserio; // Arrastra aquí el ItemData de la receta
    public Sprite spriteDespues;   // El dibujo del cupcake terminado

    private void OnMouseDown() {
        if (Inventario.Instance.DatosItemSeleccionado != null) {

            if (Inventario.Instance.DatosItemSeleccionado == objetoNeceserio) {
                EnvenenarCupcake();
            }
            else {
                Debug.Log("Este objeto no sirve para decorar el cupcake.");
            }
        }
        else {
            Debug.Log("No tienes nada seleccionado en el inventario.");
        }
    }

    void EnvenenarCupcake() {
        // Cambiamos el arte
        GetComponent<SpriteRenderer>().sprite = spriteDespues;

        if (GameManager.Instance != null) {
            GameManager.Instance.cupcakeEnvenenado = true;
        }

        // Eliminamos el objeto del inventario (para que se consuma)
        Inventario.Instance.RemoveItem(objetoNeceserio.ItemID);

        // Quitamos la selección del cursor
        Inventario.Instance.DeselectItem();

        // COMPLETAR LA TAREA
        if (TareasManager.Instance != null && taskID != "") {
            TareasManager.Instance.CompleteTask(taskID);
        }

        Debug.Log("¡Cupcake decorado y objeto consumido!");
    }
}

