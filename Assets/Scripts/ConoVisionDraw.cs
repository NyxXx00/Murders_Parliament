using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ConoVisionDraw : MonoBehaviour {

    [Header("Configuración de Visión")]
    public float distanciaVision = 5f;
    public float anguloVision = 60f;
    public LayerMask capaObstaculos;

    [Header("Referencias")]
    public DialogueTrigger dialogoNPC; // Arrastra aquí el componente de diálogo
    public Transform visualCono;      // Arrastra aquí el hijo con el Sprite

    private Transform jugador;
    private bool detectado = false;

    void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) jugador = player.transform;

        if (visualCono != null) {
            visualCono.localScale = new Vector3(distanciaVision, distanciaVision, 1);
        }
    }

    void Update() {
        if (jugador == null || detectado) return;

        Vector2 direccionMirado = transform.right;
        Vector2 direccionJugador = (jugador.position - transform.position);
        float distanciaActual = direccionJugador.magnitude;

        if (distanciaActual < distanciaVision) {
            float angulo = Vector2.Angle(direccionMirado, direccionJugador);

            if (angulo < anguloVision / 2) {
                // Comprobamos si hay paredes
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direccionJugador.normalized, distanciaActual, capaObstaculos);

                if (hit.collider == null || hit.collider.CompareTag("Player")) {
                    CapturarJugador();
                }
            }
        }
    }

    private void CapturarJugador() {
        detectado = true;

        // Detener al jugador (Asegúrate de que tu script se llama PlayerControl)
        var control = jugador.GetComponent<PlayerControl>();
        if (control != null) control.enabled = false;

        // Poner el cono rojo
        if (visualCono != null) {
            var renderer = visualCono.GetComponent<SpriteRenderer>();
            if (renderer != null) renderer.color = new Color(1, 0, 0, 0.5f);
        }

        // ACTIVAR DIÁLOGO
        if (dialogoNPC != null) {
            dialogoNPC.TriggerDialogue();
        }

        StartCoroutine(ReiniciarEscena());
    }

    private IEnumerator ReiniciarEscena() {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Vector3 frente = transform.right * distanciaVision;
        Vector3 limiteSup = Quaternion.Euler(0, 0, anguloVision / 2) * frente;
        Vector3 limiteInf = Quaternion.Euler(0, 0, -anguloVision / 2) * frente;
        Gizmos.DrawRay(transform.position, limiteSup);
        Gizmos.DrawRay(transform.position, limiteInf);
    }
}