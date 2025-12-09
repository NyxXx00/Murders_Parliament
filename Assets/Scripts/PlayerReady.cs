// Script: PlayerReadyNotifier
using UnityEngine;
using System; // Importante para el Action/Event

public class PlayerReady : MonoBehaviour {
    // El evento estático que otros scripts escucharán
    public static event Action OnPlayerSpawnedAndReady;

    void Start() {

        OnPlayerSpawnedAndReady?.Invoke();

        // Limpia el evento después de que se usa, para evitar errores si el jugador persiste.
        OnPlayerSpawnedAndReady = null;
    }
}