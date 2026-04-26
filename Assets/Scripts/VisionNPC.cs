using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisionNPC : MonoBehaviour {

    public float fovAngle = 60f;
    public float fovRange = 5f;
    public Transform rayPoint;
    public LayerMask capaObstaculos;
    public LayerMask capaJugador;

    [Header("Escena y Spawn")]
    public string nombreEscenaMuerte;
    public string idDelSpawnAlMorir; // Aquí pones el ID que tenga el script Spawn

    [Header("Configuración de Diálogo")]
    public DialogueTrigger dialogoDeteccion;
    public float esperaAntesDeEscena = 2.0f;

    private Transform target;
    private bool yaPillado = false;

    void Awake() {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) target = p.transform;
        if (rayPoint == null) rayPoint = transform;
    }

    void Update() {
        if (target == null || yaPillado) return;

        Vector2 forwardDirection = transform.right;
        Vector2 targetDirection = (target.position - rayPoint.position);
        float distanceToPlayer = targetDirection.magnitude;

        if (distanceToPlayer <= fovRange) {
            float angleToPlayer = Vector2.Angle(forwardDirection, targetDirection);

            if (angleToPlayer < fovAngle / 2) {
                RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, targetDirection.normalized, fovRange, capaObstaculos | capaJugador);

                if (hit.collider != null && hit.collider.CompareTag("Player")) {
                    StartCoroutine(SecuenciaDeteccion());
                }
            }
        }
        DibujarDebug(forwardDirection);
    }

    IEnumerator SecuenciaDeteccion() {
        yaPillado = true;

        if (dialogoDeteccion != null) {
            dialogoDeteccion.TriggerDialogue();
        }

        if (GameManager.Instance != null && !string.IsNullOrEmpty(idDelSpawnAlMorir)) {
            // Suponiendo que tu GameManager tiene un método para setear el spawn, 
            // si no, usa la variable directamente:
            GameManager.Instance.SetNextSpawnPoint(idDelSpawnAlMorir);
        }

        yield return new WaitForSeconds(esperaAntesDeEscena);

        if (!string.IsNullOrEmpty(nombreEscenaMuerte)) {
            SceneManager.LoadScene(nombreEscenaMuerte);
        }
        else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void DibujarDebug(Vector2 forward) {
        Vector3 izquierda = Quaternion.Euler(0, 0, fovAngle / 2) * forward * fovRange;
        Vector3 derecha = Quaternion.Euler(0, 0, -fovAngle / 2) * forward * fovRange;
        Debug.DrawRay(rayPoint.position, izquierda, Color.red);
        Debug.DrawRay(rayPoint.position, derecha, Color.red);
        Debug.DrawRay(rayPoint.position, (Vector3)forward * fovRange, Color.blue);
    }
}