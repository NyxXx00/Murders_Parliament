using UnityEngine;

public class Spawn : MonoBehaviour {
    public string spawnID;                  // ID de este punto de spawn
    public static bool playerPlaced = false; // Flag para la c�mara
    private void Awake() {
        playerPlaced = false;

        if (GameManager.Instance == null) return;

        string nextSpawn = GameManager.Instance.GetNextSpawnPoint();
        if (string.IsNullOrEmpty(nextSpawn)) return;

        // Si coincide la ID, coloca al jugador aqu�
        if (nextSpawn == spawnID) {
            PlayerControl player = FindFirstObjectByType<PlayerControl>();
            if (player != null) {
                player.transform.position = transform.position;
                playerPlaced = true; // ahora la c�mara puede seguirlo
            }

            GameManager.Instance.ClearSpawnPoint(); // limpia para que no se repita
        }
    }
}