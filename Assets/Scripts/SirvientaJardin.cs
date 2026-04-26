using UnityEngine;

public class SirvientaJardin : MonoBehaviour {
    public float velocidad = 2.5f;
    public GameObject objetoCarro;

    [Header("Ruta para llegar al puesto (Esquivando arbustos)")]
    public Transform[] rutaPatrullaIda; // Aquí pones los puntos para rodear los arbustos

    private Transform[] rutaActual;
    private int indiceActual = 0;
    private bool investigando = false;
    private bool paradaTotal = false;

    void Start() {
        // Al empezar, su ruta es la de ir a su puesto
        if (rutaPatrullaIda.Length > 0) {
            rutaActual = rutaPatrullaIda;
        }
    }

    void Update() {
        if (paradaTotal || rutaActual == null || rutaActual.Length == 0) return;

        Transform objetivo = rutaActual[indiceActual];
        MoverHacia(objetivo.position);

        if (Vector2.Distance(transform.position, objetivo.position) < 0.1f) {
            // Avanzar al siguiente punto de la ruta
            if (indiceActual < rutaActual.Length - 1) {
                indiceActual++;
            }
            else {
                // Si es la ruta de investigación, se para para siempre
                if (investigando) {
                    paradaTotal = true;
                }

            }
        }
    }

    void MoverHacia(Vector2 destino) {
        transform.position = Vector2.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);
        if (transform.position.x < destino.x) transform.eulerAngles = Vector3.zero;
        else transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public void EscucharRuido(Transform[] rutaHaciaPiedra) {
        if (investigando) return;

        if (objetoCarro != null) {
            objetoCarro.transform.SetParent(null);
        }

        // CAMBIO DE RUTA RADICAL
        rutaActual = rutaHaciaPiedra;
        indiceActual = 0;
        investigando = true;
        paradaTotal = false;
        Debug.Log("¡Ruido! Cambiando ruta para esquivar arbustos hacia la piedra.");
    }
}