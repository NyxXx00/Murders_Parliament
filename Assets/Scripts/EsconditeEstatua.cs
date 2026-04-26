using UnityEngine;

public class EsconditeEstatua : MonoBehaviour {
    // Statica para que los enemigos puedan consultarla fácilmente
    public static bool jugadorEstaEscondido = false;

    private bool jugadorEnRango = false;
    private SpriteRenderer jugadorSprite;
    private Color colorOriginal;

    void Update() {
        // Si el jugador está en el área y pulsa la tecla (ejemplo: E)
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.Q)) {
            AlternarEscondite();
        }
    }

    void AlternarEscondite() {
        jugadorEstaEscondido = !jugadorEstaEscondido;

        if (jugadorEstaEscondido) {

            colorOriginal = jugadorSprite.color;
            jugadorSprite.color = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, 0.2f);

            Debug.Log("Escondido. Los enemigos no te ven.");
        }
        else {
            // Volver a la normalidad
            jugadorSprite.color = colorOriginal;
            Debug.Log("Ya no estás escondido.");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            jugadorEnRango = true;
            jugadorSprite = other.GetComponent<SpriteRenderer>();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            jugadorEnRango = false;
            // Si el jugador sale del área andando, deja de estar escondido
            if (jugadorEstaEscondido) {
                AlternarEscondite();
            }
        }
    }
}