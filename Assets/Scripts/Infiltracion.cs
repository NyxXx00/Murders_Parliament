using UnityEngine;

public class Infiltracion : MonoBehaviour {

    public GameObject zonaImpacto; // La flecha
    public SirvientaJardin[] sirvientas;
    public Transform[] rutaALaCocina; // Los puntos que esquivan los setos

    public void LanzarPiedra() {
        // Comprobar que estamos escondidos
        if (EsconditeEstatua.jugadorEstaEscondido) {
            Debug.Log("¡Piedra lanzada! Las sirvientas se mueven por la ruta segura.");

            foreach (SirvientaJardin s in sirvientas) {
                if (s != null) s.EscucharRuido(rutaALaCocina);
            }

            if (zonaImpacto != null) zonaImpacto.SetActive(false);
        }
        else {
            Debug.Log("Estás a la vista, no puedes lanzar la piedra.");
        }
    }
}