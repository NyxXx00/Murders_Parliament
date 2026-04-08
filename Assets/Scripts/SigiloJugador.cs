using UnityEngine;

public class SigiloJugador : MonoBehaviour {
    // Esta variable te dirá si estás a salvo (sombras) o no (luz)
    public bool estaOculto = true;

    // Opcional: Para cambiar el color del sprite y 'verlo' mejor
    private SpriteRenderer rend;

    void Awake() {
        rend = GetComponent<SpriteRenderer>();
        // Empezamos un poco oscuros para dar sensación de 'sombra'
        rend.color = new Color(0.5f, 0.5f, 0.5f);
    }

    // Al pisar los círculos amarillos de tu imagen (Tag 'LuzFarola')
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("LuzFarola")) {
            estaOculto = false; // ¡PILLADO!
            rend.color = Color.white; // Brilla para avisar al jugador
            Debug.Log("¡El jugador está visible en la farola!");
        }
    }

    // Al salir de los círculos amarillos y volver al verde/gris oscuro
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("LuzFarola")) {
            estaOculto = true; // OCULTO DE NUEVO
            rend.color = new Color(0.5f, 0.5f, 0.5f); // Se oscurece en la sombra
            Debug.Log("¡El jugador vuelve a las sombras!");
        }
    }
}