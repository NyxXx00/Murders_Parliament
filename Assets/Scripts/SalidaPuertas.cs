using UnityEngine;
using UnityEngine.SceneManagement;

public class SalidaPuertas : MonoBehaviour {

    [Header("Configuración de Escena")]
    public string nextSceneName;
    public string destinationPointID;

    [Header("Configuración de Llave (Opcional)")]
    [Tooltip("Si dejas esto vacío, la puerta se abrirá siempre. Si pones un ID, necesitará la llave.")]
    public string idLlaveRequerida;

    [Tooltip("Diálogo que saldrá si no tienes la llave")]
    public DialogueTrigger triggerBloqueado;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            //si no requiere llave, pasa directamente
            if (string.IsNullOrEmpty(idLlaveRequerida)) {
                ChangeScene();
            }
            // si requiere llave, comprobamos qué tiene seleccionado el jugador
            else {
                // Comprobamos si el item SELECCIONADO actualmente es el correcto
                if (Inventario.Instance != null &&
                    Inventario.Instance.DatosItemSeleccionado != null &&
                    Inventario.Instance.DatosItemSeleccionado.ItemID == idLlaveRequerida) {

                    Debug.Log("Uso de llave correcto.");

                    //eliminar la llave tras usarla para que no pueda entrar más o se gaste
                    Inventario.Instance.RemoveItem(idLlaveRequerida);

                    ChangeScene();
                }
                else {
                    // si no tiene nada seleccionado o es el objeto equivocado
                    Debug.Log("La puerta no cede. Quizás si selecciono la llave adecuada...");

                    if (triggerBloqueado != null) {
                        triggerBloqueado.TriggerDialogue();
                    }
                }
            }
        }
    }

    public void ChangeScene() {
        if (GameManager.Instance != null) {
            GameManager.Instance.SetNextSpawnPoint(destinationPointID);
        }

        SceneManager.LoadScene(nextSceneName);
        Debug.Log($"Cambiando a escena: {nextSceneName}. Spawn ID guardado: {destinationPointID}");
    }
}