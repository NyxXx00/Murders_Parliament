using UnityEngine;

public class SirvientaJardin : MonoBehaviour {
    [Header("Configuración de Patrulla")]
    [SerializeField] private float velocidadMov;
    [SerializeField] private Transform[] puntosPatrulla;
    [SerializeField] private float distanciaMinima = 0.2f;
    [SerializeField] private float tiempoEspera;

    [Header("Ruta de Distracción")]
    private Transform[] puntosDistraccion;
    private bool modoDistraccion = false;

    private int indiceActual = 0;
    private float tiempoEsperaActual;

    private void Start() {
        tiempoEsperaActual = tiempoEspera;
        if (puntosPatrulla.Length > 0) Girar(puntosPatrulla[indiceActual].position);
    }

    private void Update() {
        if (!modoDistraccion && puntosPatrulla.Length == 0) return;

        Vector3 objetivo = modoDistraccion ? puntosDistraccion[indiceActual].position : puntosPatrulla[indiceActual].position;

        // Movimiento
        transform.position = Vector2.MoveTowards(transform.position, objetivo, velocidadMov * Time.deltaTime);

        // Si llega al punto
        if (Vector2.Distance(transform.position, objetivo) < distanciaMinima) {
            if (modoDistraccion) {
                if (indiceActual < puntosDistraccion.Length - 1) {
                    indiceActual++;
                    Girar(puntosDistraccion[indiceActual].position);
                }
                // Si llega al último, se queda ahí (no hacemos nada)
            }
            else {
                // Lógica de espera para patrulla normal
                if (tiempoEsperaActual > 0) {
                    tiempoEsperaActual -= Time.deltaTime;
                }
                else {
                    indiceActual = (indiceActual + 1) % puntosPatrulla.Length;
                    tiempoEsperaActual = tiempoEspera;
                    Girar(puntosPatrulla[indiceActual].position);
                }
            }
        }
    }

    public void EscucharRuido(Transform[] rutaHaciaCocina) {
        puntosDistraccion = rutaHaciaCocina;
        modoDistraccion = true;
        indiceActual = 0;
        tiempoEsperaActual = 0;
        Girar(puntosDistraccion[indiceActual].position);
    }

    private void Girar(Vector3 destino) {
        if (transform.position.x < destino.x) transform.eulerAngles = new Vector3(0, 0, 0);
        else transform.eulerAngles = new Vector3(0, 180, 0);
    }
}