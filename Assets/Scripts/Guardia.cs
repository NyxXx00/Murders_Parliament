using UnityEngine;

public class Guardia : MonoBehaviour {
    public Transform puntoInvestigacion; // Un objeto vacío donde debe ir
    public float velocidad = 3f;
    private bool moviendose = false;

    // Esta es la función que llama el Jarrón
    public void EmpezarAMoverse() {
        moviendose = true;
        Debug.Log("Guardia: ¡He oído un ruido! Voy a investigar.");
    }

    void Update() {
        // Solo nos movemos si se ha llamado a EmpezarAMoverse()
        if (moviendose && puntoInvestigacion != null) {
            transform.position = Vector2.MoveTowards(
                transform.position,
                puntoInvestigacion.position,
                velocidad * Time.deltaTime
            );

            // Si llegamos, nos detenemos
            if (Vector2.Distance(transform.position, puntoInvestigacion.position) < 0.1f) {
                moviendose = false;
                Debug.Log("Guardia ha llegado a la escena del crimen.");
            }
        }
    }
}