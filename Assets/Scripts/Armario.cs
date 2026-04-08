using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class Armario : MonoBehaviour {
    [Header("Sprites de Animación")]
    public Sprite spriteCerrado;
    public Sprite spriteMedio;
    public Sprite spriteAbierto;

    private SpriteRenderer sr;
    private bool estaDentro = false;
    private bool jugadorCerca = false;
    private GameObject elvira;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = spriteCerrado;
    }

    void Update() {
        // Si pulsas F y estás cerca O ya estás dentro (para poder salir)
        if ((jugadorCerca || estaDentro) && Input.GetKeyDown(KeyCode.F)) {
            StartCoroutine(SecuenciaArmario());
        }
    }

    System.Collections.IEnumerator SecuenciaArmario() {
        sr.sprite = spriteMedio;
        yield return new WaitForSeconds(0.1f);
        sr.sprite = spriteAbierto;
        yield return new WaitForSeconds(0.1f);

        TogglePersonaje();

        sr.sprite = spriteMedio;
        yield return new WaitForSeconds(0.1f);
        sr.sprite = spriteCerrado;
    }

    void TogglePersonaje() {
        estaDentro = !estaDentro;

        if (estaDentro) {
            // ENTRAR: Nos escondemos y apagamos el control
            elvira.GetComponent<SpriteRenderer>().enabled = false;
            elvira.GetComponent<PlayerControl>().enabled = false;
            elvira.GetComponent<Rigidbody2D>().simulated = false;

            ActivarEventoSirvienta();
        }
        else {

            elvira.GetComponent<SpriteRenderer>().enabled = true;

            // ACTIVAMOS TU SCRIPT DE MOVIMIENTO (WASD / Flechas)
            PlayerControl scriptMovimiento = elvira.GetComponent<PlayerControl>();
            if (scriptMovimiento != null) {
                scriptMovimiento.enabled = true;
            }

            Rigidbody2D rb = elvira.GetComponent<Rigidbody2D>();
            if (rb != null) {
                rb.simulated = true;
            }

            Debug.Log("Control manual recuperado. ¡Lleva a Elvira a la puerta!");
        }
    }

    void ActivarEventoSirvienta() {
        // Buscamos el objeto por nombre (asegúrate de que se llame así en la Hierarchy)
        GameObject sirvienta = GameObject.Find("Sirvientes_Carro_0");
        if (sirvienta != null) {
            sirvienta.SetActive(true);
            Debug.Log("La sirvienta ha aparecido.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            jugadorCerca = true;
            elvira = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        // Solo pierde la cercanía si NO está dentro
        if (other.gameObject == elvira && !estaDentro) {
            jugadorCerca = false;
        }
    }
}