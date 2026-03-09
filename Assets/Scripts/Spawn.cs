using UnityEngine;

public class Spawn : MonoBehaviour {
    public string spawnID;                  // ID de este punto de spawn
<<<<<<< HEAD
    public static bool playerPlaced = false; // Flag para la cï¿½mara
=======
    public static bool playerPlaced = false; // Flag para la cámara
>>>>>>> 998451dad077bfbe3afa4da851f744ad535d8fa7
    private void Awake() {
        playerPlaced = false;

        if (GameManager.Instance == null) return;

        string nextSpawn = GameManager.Instance.GetNextSpawnPoint();
        if (string.IsNullOrEmpty(nextSpawn)) return;

<<<<<<< HEAD
        // Si coincide la ID, coloca al jugador aquï¿½
=======
        // Si coincide la ID, coloca al jugador aquí
>>>>>>> 998451dad077bfbe3afa4da851f744ad535d8fa7
        if (nextSpawn == spawnID) {
            PlayerControl player = FindFirstObjectByType<PlayerControl>();
            if (player != null) {
                player.transform.position = transform.position;
<<<<<<< HEAD
                playerPlaced = true; // ahora la cï¿½mara puede seguirlo
=======
                playerPlaced = true; // ahora la cámara puede seguirlo
>>>>>>> 998451dad077bfbe3afa4da851f744ad535d8fa7
            }

            GameManager.Instance.ClearSpawnPoint(); // limpia para que no se repita
        }
    }
}