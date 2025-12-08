using UnityEngine;
using System.Collections;

public class EstadoCupcake : MonoBehaviour {

    // Componentes y referencias visuales
    private Renderer objectRenderer;
    private Color originalColor;

    // Opcional: Color para el estado envenenado
    public Color colorEnvenenado = new Color(0.2f, 0.5f, 0.2f, 1f); // Color verde oscuro

    [HideInInspector]
    public bool haSidoEnvenenado = false;

    void Start() {
        // obtener el Renderer
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null) {
            // guardar el color original 
            originalColor = objectRenderer.material.color;
        }
        else {
            Debug.LogError("EstadoCupcake no encontró un componente Renderer. No se podrá cambiar el color.", this);
        }
    }

    //llamado por ObjetoInteractuable cuando se usa el ítem correcto
    public void MarcarComoEnvenenado() {
        if (haSidoEnvenenado) return;

        // cambiar el estado
        haSidoEnvenenado = true;

        //aplicar el color de envenenado
        if (objectRenderer != null) {
            objectRenderer.material.color = colorEnvenenado;

        }

    }

}