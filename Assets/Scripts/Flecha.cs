using UnityEngine;

public class PiedraRecompensa : MonoBehaviour {
    private void OnMouseDown() {
        Infiltracion manejador = Object.FindFirstObjectByType<Infiltracion>();

        if (manejador != null) {
            manejador.LanzarPiedra();
            Debug.Log("Piedra lanzada. ¡Corre a esconderte!");
            gameObject.SetActive(false);
        }
    }

}