using UnityEngine;

public class MovimientoSirvienta : MonoBehaviour {
    public Transform destinoAceite;
    public float velocidad = 1f;

    [Header("Referencia al Jarrón")]
    public Jarron scriptJarron; // Arrastra aquí el objeto que tiene el script Jarron

    private bool haResbalado = false;

    void Update() {
        if (destinoAceite != null && !haResbalado) {
            transform.position = Vector2.MoveTowards(
                transform.position,
                destinoAceite.position,
                velocidad * Time.deltaTime
            );

            // Si está muy cerca del aceite
            if (Vector2.Distance(transform.position, destinoAceite.position) < 0.1f) {
                Resbalar();
            }
        }
    }

    void Resbalar() {
        haResbalado = true;

        if (scriptJarron != null) {
            scriptJarron.Romper();
            Debug.Log("La sirvienta ha llamado a la función Romper del Jarrón.");
        }
        else {
            Debug.LogError("¡Olvidasste arrastrar el Jarrón al script de la Sirvienta!");
        }
    }
}