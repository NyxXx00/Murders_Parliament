// Camara.cs
using UnityEngine;

public class Camara : MonoBehaviour {
    private Transform player;

    void Start() {
        // Busca al jugador al inicio
        PlayerControl p = FindFirstObjectByType<PlayerControl>();
        if (p != null)
            player = p.transform;
    }

    void LateUpdate() {
        // Si aún no tiene referencia al jugador, búscalo
        if (player == null) {
            PlayerControl p = FindFirstObjectByType<PlayerControl>();
            if (p != null)
                player = p.transform;
            else
                return;
        }

        // Solo sigue al jugador después de que Spawn lo haya colocado
        if (!Spawn.playerPlaced) return;

        // Sigue al jugador, manteniendo la Z de la cámara
        transform.position = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z
        );
    }
}