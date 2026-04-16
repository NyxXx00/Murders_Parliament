using UnityEngine;

public class Placa : MonoBehaviour {
    public static Placa Instance; // Nuevo: Para que el Maletín te encuentre

    [Header("Configuración de UI")]
    public GameObject panelPlaca;

    [Header("Ajustes")]
    public float distanciaMaxima = 3f;

    void Awake() {
        if (Instance == null) Instance = this;
    }

    void OnMouseDown() {
        // SEGURO: Si el maletín está abierto, no abras la placa
        if (Maletin.Instance != null && Maletin.Instance.panelTeclado.activeSelf) return;

        // Lógica normal del jugador...
        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null) {
            float dist = Vector2.Distance(transform.position, jugador.transform.position);
            if (dist < distanciaMaxima) AbrirPanel();
        }
    }

    public void AbrirPanel() {
        if (panelPlaca != null) panelPlaca.SetActive(true);
    }

    public void CerrarPanel() {
        if (panelPlaca != null) panelPlaca.SetActive(false);
    }
}