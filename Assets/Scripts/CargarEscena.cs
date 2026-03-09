    using System.Linq;
    using UnityEngine;

    public class CargarEscena : MonoBehaviour
    {
        void Start() {
            // Obten la id que el GameManager guardo
            string spawnPointID = GameManager.Instance.GetNextSpawnPoint();

            // Si no hay id el personaje se queda donde est�
            if (string.IsNullOrEmpty(spawnPointID)) {
                Debug.Log("No se encontr� un id de spawn");
                return;
            }

            // Buscar el objeto SpawnPoint que coincide con la id guardada.
            Spawn targetSpawn = FindObjectsByType<Spawn>(FindObjectsSortMode.None)
                                      .FirstOrDefault(sp => sp.spawnID == spawnPointID);

            if (targetSpawn != null) {
                // mover al jugador a la posici�n del punto de spawn encontrado.
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null) {
                    player.transform.position = targetSpawn.transform.position;
                    Debug.Log($"Jugador movido a {spawnPointID} ({targetSpawn.transform.position})");
                }
            }
            else {
                Debug.LogError($"�ERROR! No se encontr� un SpawnPoint con la ID: {spawnPointID}");
            }

            // Limpia la id para evitar errores
            GameManager.Instance.ClearSpawnPoint();
        }
    }
