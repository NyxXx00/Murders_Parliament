using UnityEngine;
using System.Collections;

public class AudioDestiladora : MonoBehaviour {

    // Variables públicas para configurar en el Inspector
    public Color clickedColor = Color.red; // Color al que cambiará

    // Componentes privados
    private AudioSource audioSource;
    private Renderer objectRenderer;
    private Color originalColor;

    // controla el temporizador de reseteo
    private Coroutine resetCoroutine;

    void Start() {
        // 1. Obtener el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            Debug.LogError("ClickableSound requiere un componente Audio Source.", this);
        }

        // obtener el Renderer y guardar el color original
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null) {
            // Guardar el color original del material al inicio del juego
            originalColor = objectRenderer.material.color;
        }
        else {
            Debug.LogError("ClickableSound no encontró un componente Renderer. Asegúrate de tener MeshRenderer o SpriteRenderer.", this);
        }
    }

    void OnMouseDown() {

        // Si ya hay un proceso de reseteo corriendo, lo detenemos para iniciar uno nuevo.
        if (resetCoroutine != null) {
            StopCoroutine(resetCoroutine);
        }

        //emitir el ruido
        if (audioSource != null && audioSource.clip != null && !audioSource.isPlaying) {
            audioSource.Play();
        }

        //cambiar el color del objeto al color de clic
        if (objectRenderer != null) {
            objectRenderer.material.color = clickedColor;
        }

        // iniciar el temporizador para resetear el color después de 10 segundos
        resetCoroutine = StartCoroutine(ResetColorAfterDelay(10f));
    }

    // corutina para resetear el color después de un retraso
    private IEnumerator ResetColorAfterDelay(float delay) {
        // Esperar la duración especificada (6 segundos)
        yield return new WaitForSeconds(delay);

        // Restablecer el color
        if (objectRenderer != null) {
            objectRenderer.material.color = originalColor;
        }

        // El temporizador se ha completado, lo ponemos a null
        resetCoroutine = null;
    }
}
