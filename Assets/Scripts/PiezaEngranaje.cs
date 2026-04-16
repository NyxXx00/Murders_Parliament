using UnityEngine;
using UnityEngine.UI;

public class PiezaEngranaje : MonoBehaviour {
    [Header("Posición de Victoria (Escríbela en el Inspector)")]
    public Vector2 posicionCorrecta;
    public bool esHuecoVacio = false;

    [Header("Ajustes de Movimiento")]
    public float velocidadMover = 1500f;
    private Vector2 objetivoPos;
    private RectTransform rectTransform;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start() {
        // La pieza empieza queriendo estar donde está en la escena
        objetivoPos = rectTransform.anchoredPosition;

        // Si es el hueco, ocultamos la imagen
        if (esHuecoVacio) {
            Image img = GetComponent<Image>();
            if (img != null) img.enabled = false;
        }
    }

    void Update() {
        // Movimiento suave hacia el objetivo
        if (rectTransform.anchoredPosition != objetivoPos) {
            rectTransform.anchoredPosition = Vector2.MoveTowards(
                rectTransform.anchoredPosition,
                objetivoPos,
                velocidadMover * Time.deltaTime
            );
        }
    }

    public void AlHacerClic() {
        if (!esHuecoVacio) {
            PuzzleEngranajesManager.Instance.IntentarMover(this);
        }
    }

    public void MoverA(Vector2 nuevaPos) {
        objetivoPos = nuevaPos;
    }

    public bool EstaEnSuSitio() {
        // Si la distancia es menor de 50 píxeles, la damos por buena.
        // Esto evita que los decimales como 105.55 te bloqueen la victoria.
        float d = Vector2.Distance(rectTransform.anchoredPosition, posicionCorrecta);
        return d < 50f;
    }
}