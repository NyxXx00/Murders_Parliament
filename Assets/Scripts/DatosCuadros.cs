using System.Collections.Generic;
using UnityEngine;

public class DatosCuadros : MonoBehaviour {

    public static DatosCuadros Instance;

    // Diccionario para guardar qué ItemID hay en cada hueco (por índice)
    public Dictionary<int, string> estadoCuadros = new Dictionary<int, string>();
    public bool puzzleCompletado = false;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
