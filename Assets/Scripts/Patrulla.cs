using UnityEngine;

public class Patrulla : MonoBehaviour {

    [SerializeField] private float velocidadMov;
    [SerializeField] private Transform[] puntoMov;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private float tiempoEspera;

    private float tiempoEsperaActual;
    private int siguientePaso;
    private SpriteRenderer spriteRenderer;

    // --- NUEVA REFERENCIA ---
    private VisionNPC miVision;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        miVision = GetComponent<VisionNPC>(); // Buscamos el script de visión
        tiempoEsperaActual = tiempoEspera;
        GirarH();
    }

    private void Update() {
        if (puntoMov.Length == 0) return;

        if (tiempoEsperaActual > 0) {
            tiempoEsperaActual -= Time.deltaTime;
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            puntoMov[siguientePaso].position,
            velocidadMov * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, puntoMov[siguientePaso].position) < distanciaMinima) {
            tiempoEsperaActual = tiempoEspera;
            siguientePaso++;
            if (siguientePaso >= puntoMov.Length) {
                siguientePaso = 0;
            }
            GirarH();
        }
    }

    private void GirarH() {
        // Si el siguiente punto está a la derecha
        if (transform.position.x < puntoMov[siguientePaso].position.x) {
            transform.eulerAngles = new Vector3(0, 0, 0);

            // --- NUEVO: Decirle a la visión que mire a la derecha ---
            // En Unity, transform.right es la dirección "adelante" del objeto
            // Al rotar el objeto a 0,0,0, transform.right ya apunta a la derecha
        }
        // Si el siguiente punto está a la izquierda
        else {
            transform.eulerAngles = new Vector3(0, 180, 0);

            // --- NUEVO: Al rotar el objeto 180 grados en Y, 
            // transform.right ahora apunta automáticamente a la izquierda
        }
    }
}