using UnityEngine;

public class Bustos : MonoBehaviour {
    public GameObject panelMosaico; // Arrastra aquí el Canvas del Panel que me enseñaste

    void OnMouseDown() {
        // Al clicar el busto en el mundo 2D, se activa el panel de la UI
        if (panelMosaico != null) {
            panelMosaico.SetActive(true);

            // Opcional: Sonido de piedra moviéndose al abrir
            // AudioSource.PlayClipAtPoint(sonidoAbrir, transform.position);
        }
    }
}
