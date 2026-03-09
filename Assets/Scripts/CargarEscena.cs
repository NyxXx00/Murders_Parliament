    using System.Linq;
    using UnityEngine;

    public class CargarEscena : MonoBehaviour
    {
        void Start() {
            // Obten la id que el GameManager guardo
            string spawnPointID = GameManager.Instance.GetNextSpawnPoint();

<<<<<<< HEAD
            // Si no hay id el personaje se queda donde estï¿½
            if (string.IsNullOrEmpty(spawnPointID)) {
                Debug.Log("No se encontrï¿½ un id de spawn");
=======
            // Si no hay id el personaje se queda donde está
            if (string.IsNullOrEmpty(spawnPointID)) {
                Debug.Log("No se encontró un id de spawn");
>>>>>>> 998451dad077bfbe3afa4da851f744ad535d8fa7
                return;
            }

            // Buscar el objeto SpawnPoint que coincide con la id guardada.
            Spawn targetSpawn = FindObjectsByType<Spawn>(FindObjectsSortMode.None)
                                      .FirstOrDefault(sp => sp.spawnID == spawnPointID);

            if (targetSpawn != null) {
<<<<<<< HEAD
                // mover al jugador a la posiciï¿½n del punto de spawn encontrado.
=======
                // mover al jugador a la posición del punto de spawn encontrado.
>>>>>>> 998451dad077bfbe3afa4da851f744ad535d8fa7
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null) {
                    player.transform.position = targetSpawn.transform.position;
                    Debug.Log($"Jugador movido a {spawnPointID} ({targetSpawn.transform.position})");
                }
            }
            else {
<<<<<<< HEAD
                Debug.LogError($"ï¿½ERROR! No se encontrï¿½ un SpawnPoint con la ID: {spawnPointID}");
=======
                Debug.LogError($"¡ERROR! No se encontró un SpawnPoint con la ID: {spawnPointID}");
>>>>>>> 998451dad077bfbe3afa4da851f744ad535d8fa7
            }

            // Limpia la id para evitar errores
            GameManager.Instance.ClearSpawnPoint();
        }
    }
