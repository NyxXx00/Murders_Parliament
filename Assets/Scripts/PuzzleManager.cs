using UnityEngine;

public class PuzzleManager : MonoBehaviour {
    public static PuzzleManager Instance;

    [Header("Referencias de la Escena")]
    [Tooltip("Arrastra los 4 objetos 'Cuadro' de la jerarquía en orden de izquierda a derecha")]
    public Cuadros[] huecos;

    [Header("Solución")]
    [Tooltip("Arrastra los ItemData (Assets) en el orden correcto")]
    public ItemData[] ordenCorrecto;

    public GameObject llaveRecompensa;

    void Awake() { Instance = this; }

    public void ComprobarOrden() {
        bool victoria = true;

        for (int i = 0; i < huecos.Length; i++) {
            // Si el hueco está vacío o el item no coincide con la solución
            if (huecos[i].estaVacio || huecos[i].cuadroData != ordenCorrecto[i]) {
                victoria = false;
                break;
            }
        }

        if (victoria) {
            Ganar();
        }
    }

    void Ganar() {
        Debug.Log("¡Puzzle completado!");
        llaveRecompensa.SetActive(true);
        // Bloqueamos los cuadros para que no se puedan volver a quitar
        foreach (var h in huecos) h.enabled = false;
    }
}