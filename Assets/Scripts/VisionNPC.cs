using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VisionNPC : MonoBehaviour {

    public float fovAngle = 60f;
    public float fovRange = 5f;
    public Transform rayPoint;
    public LayerMask capaObstaculos;
    public LayerMask capaJugador;

    private Transform target;
    [HideInInspector] public Vector2 lookDirection = Vector2.right; // Dirección hacia la que mira

    void Awake() {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) target = p.transform;
    }

    void Update() {
        if (target == null) return;

        lookDirection = transform.right;
        Vector2 targetDirection = (target.position - rayPoint.position);
        float angleToPlayer = Vector2.Angle(lookDirection, targetDirection);

        if (angleToPlayer < fovAngle / 2) {
            RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, targetDirection.normalized, fovRange, capaObstaculos | capaJugador);

            if (hit.collider != null && hit.collider.CompareTag("Player")) {
                Debug.Log("¡Pillado!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        // Dibujamos el cono en la ventana Scene para que veas que GIRA
        DibujarDebug();
    }

    void DibujarDebug() {
        Vector3 izquierda = Quaternion.Euler(0, 0, fovAngle / 2) * lookDirection * fovRange;
        Vector3 derecha = Quaternion.Euler(0, 0, -fovAngle / 2) * lookDirection * fovRange;
        Debug.DrawRay(rayPoint.position, izquierda, Color.blue);
        Debug.DrawRay(rayPoint.position, derecha, Color.blue);
    }
}