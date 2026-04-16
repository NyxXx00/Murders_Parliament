using System.Collections;
using UnityEngine;

public class Armario : MonoBehaviour {
    [Header("Referencias Visuales")]
    public Sprite spriteCerrado;
    public Sprite spriteMedio;
    public Sprite spriteAbierto;
    public GameObject iconoTecla; // El objeto visual de la "F"

    [Header("Ajustes de Interacción")]
    public float radioDeteccion = 1.5f;
    public KeyCode teclaInteraccion = KeyCode.F;
    public int taskID;

    [Header("Estado")]
    public bool puzzleResuelto = false;

    private SpriteRenderer sr;
    private bool estaDentro = false;
    private bool jugadorCerca = false;
    private Transform jugador;
    private GameObject elvira;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = spriteCerrado;

        // Buscamos al jugador por Tag como haces en tus NPCs
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) {
            jugador = playerObj.transform;
            elvira = playerObj;
        }

        if (iconoTecla != null) iconoTecla.SetActive(false);
    }

    void Update() {
        if (jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        // Lógica de rango (Solo si el puzzle está resuelto)
        if (puzzleResuelto && distancia <= radioDeteccion && !estaDentro) {
            if (!jugadorCerca) {
                jugadorCerca = true;
                if (iconoTecla != null) iconoTecla.SetActive(true);
            }

            if (Input.GetKeyDown(teclaInteraccion)) {
                StartCoroutine(SecuenciaArmario());
            }
        }
        else {
            if (jugadorCerca) {
                jugadorCerca = false;
                if (iconoTecla != null) iconoTecla.SetActive(false);
            }

            // Si estamos dentro y pulsamos F, salimos
            if (estaDentro && Input.GetKeyDown(teclaInteraccion)) {
                StartCoroutine(SecuenciaArmario());
            }
        }
    }

    IEnumerator SecuenciaArmario() {
        // Ocultamos el icono mientras ocurre la animación
        if (iconoTecla != null) iconoTecla.SetActive(false);

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
            elvira.GetComponent<SpriteRenderer>().enabled = false;
            elvira.GetComponent<PlayerControl>().enabled = false;
            elvira.GetComponent<Rigidbody2D>().simulated = false;

            if (TareasManager.Instance != null && taskID != 0) {
                TareasManager.Instance.CompleteTask(taskID);
            }
            ActivarEventoSirvienta();
        }
        else {
            elvira.GetComponent<SpriteRenderer>().enabled = true;
            elvira.GetComponent<PlayerControl>().enabled = true;
            elvira.GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    void ActivarEventoSirvienta() {
        GameObject sirvienta = GameObject.Find("Sirvientes_Carro_0");
        if (sirvienta != null) sirvienta.SetActive(true);
    }
}