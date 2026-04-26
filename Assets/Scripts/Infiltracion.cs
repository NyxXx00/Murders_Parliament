using UnityEngine;

public class Infiltracion : MonoBehaviour {

    public GameObject zonaImpacto; // La flecha
    public SirvientaJardin[] sirvientas;
    public Transform[] rutaALaCocina; // Los puntos que esquivan los setos

    public void LanzarPiedra() {
        Debug.Log("Intentando mover a las sirvientas...");

        // Si la lista de sirvientas está vacía, esto las busca automáticamente
        if (sirvientas == null || sirvientas.Length == 0) {
            sirvientas = Object.FindObjectsByType<SirvientaJardin>(FindObjectsSortMode.None);
        }

        foreach (SirvientaJardin s in sirvientas) {
            if (s != null) {
                Debug.Log("Avisando a: " + s.name);
                s.EscucharRuido(rutaALaCocina);
            }
        }
    }
}