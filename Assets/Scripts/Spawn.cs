using UnityEngine;

public class Spawn : MonoBehaviour {
    public string spawnID;

    void Start() {
        if (GameManager.Instance == null) return;

        string nextSpawn = GameManager.Instance.GetNextSpawnPoint();

        // Si no hay spawn guardado, no reposiciona
        if (string.IsNullOrEmpty(nextSpawn)) return;

        if (nextSpawn == spawnID) {
            PlayerControl player = FindFirstObjectByType<PlayerControl>();
            if (player != null) {
                player.transform.position = transform.position;
            }

            // Limpia la ID para evitar reposicionar otra vez
            GameManager.Instance.ClearSpawnPoint();
        }
    }
}
