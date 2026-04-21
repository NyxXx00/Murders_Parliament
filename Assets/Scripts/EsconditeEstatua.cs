using UnityEngine;

public class EsconditeEstatua : MonoBehaviour {
    public static bool jugadorEstaEscondido = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) jugadorEstaEscondido = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) jugadorEstaEscondido = false;
    }
}