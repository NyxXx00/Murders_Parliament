using UnityEngine;

public class PuzzleManager : MonoBehaviour {
    public static PuzzleManager Instance;

    public int taskID;

    [Header("Referencias de la Escena")]
    public Cuadros[] huecos;

    [Header("Solución")]
    public ItemData[] ordenCorrecto;

    public GameObject llaveRecompensa;

    void Awake() { Instance = this; }

    void Start() {
        CargarEstado();
    }

    public void ComprobarOrden() {
        bool victoria = true;
        for (int i = 0; i < huecos.Length; i++) {
            if (huecos[i].estaVacio || huecos[i].cuadroData != ordenCorrecto[i]) {
                victoria = false;
                break;
            }
        }

        if (victoria) Ganar();
    }

    void Ganar() {
        Debug.Log("¡Puzzle completado!");
        if (llaveRecompensa != null) llaveRecompensa.SetActive(true);

        if (TareasManager.Instance != null && taskID != 0) {
            TareasManager.Instance.CompleteTask(taskID);
        }

        if (DatosCuadros.Instance != null) {
            DatosCuadros.Instance.puzzleCompletado = true;
        }

        foreach (var h in huecos) h.enabled = false;
    }

    public void GuardarEstado() {
        if (DatosCuadros.Instance == null) return;

        for (int i = 0; i < huecos.Length; i++) {
            // Guardamos el ItemID o un string vacío si no hay nada
            DatosCuadros.Instance.estadoCuadros[i] = huecos[i].estaVacio ? "" : huecos[i].cuadroData.ItemID;
        }
    }

    void CargarEstado() {
        if (DatosCuadros.Instance == null) return;

        // 1. PRIMERO: Reconstruimos la pared con lo que haya en el diccionario
        // Esto hace que, aunque hayas ganado, los cuadros aparezcan en su sitio.
        for (int i = 0; i < huecos.Length; i++) {
            if (DatosCuadros.Instance.estadoCuadros.ContainsKey(i)) {
                string id = DatosCuadros.Instance.estadoCuadros[i];

                if (!string.IsNullOrEmpty(id)) {
                    ItemData encontrado = Inventario.Instance.GetItemByID(id);
                    if (encontrado != null) {
                        huecos[i].cuadroData = encontrado;
                        huecos[i].estaVacio = false;
                    }
                }
                // Actualizamos la imagen del cuadro (o lo dejamos vacío si no había nada)
                huecos[i].ActualizarVisual();
            }
        }

        // 2. SEGUNDO: Si el puzzle ya se ganó, aplicamos los efectos de victoria
        if (DatosCuadros.Instance.puzzleCompletado) {
            Debug.Log("Cargando escena: El puzzle ya estaba resuelto.");

            // Activamos la recompensa (llave, puerta, etc.)
            if (llaveRecompensa != null) {
                llaveRecompensa.SetActive(true);
            }

            // Desactivamos los scripts de los cuadros para que no se puedan mover más
            foreach (var h in huecos) {
                h.enabled = false;
            }
        }
    }
}