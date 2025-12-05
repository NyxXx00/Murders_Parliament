using UnityEngine;
using UnityEngine.SceneManagement;

public class SalidaPuertas : MonoBehaviour {

    [Header("Configuración de Escena")]
    // Nombre de la escena de destino
    public string nextSceneName;

    // Id del punto donde debe aparecer el jugador en la escena de destino
    public string destinationPointID;

    // Detecta colisiones con el jugador
    private void OnTriggerEnter2D(Collider2D other) {
        // Verifica si el objeto que tocó la puerta tiene el tag "Player"
        if (other.CompareTag("Player")) {
            // Si es el jugador, ejecuta la lógica de cambio de escena
            ChangeScene();
        }
    }

    public void ChangeScene() {
        //Guarda la id del punto de spawn para la escena de destino
        if (GameManager.Instance != null) {
            GameManager.Instance.SetNextSpawnPoint(destinationPointID);
        }

        //Carga la escena de destino
        SceneManager.LoadScene(nextSceneName);

        Debug.Log($"Cambiando a escena: {nextSceneName}. Spawn ID guardado: {destinationPointID}");
    }
}
