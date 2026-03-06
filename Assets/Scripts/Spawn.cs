using UnityEngine;

public class Spawn : MonoBehaviour {
    public string spawnID;                  // ID de este punto de spawn
    public static bool playerPlaced = false; // Flag para la cámara
    private void Awake() {
        playerPlaced = false;

        if (GameManager.Instance == null) return;

        string nextSpawn = GameManager.Instance.GetNextSpawnPoint();
        if (string.IsNullOrEmpty(nextSpawn)) return;

        // Si coincide la ID, coloca al jugador aquí
        if (nextSpawn == spawnID) {
            PlayerControl player = FindFirstObjectByType<PlayerControl>();
            if (player != null) {
                player.transform.position = transform.position;
                playerPlaced = true; // ahora la cámara puede seguirlo
            }

            GameManager.Instance.ClearSpawnPoint(); // limpia para que no se repita
        }
    }
}