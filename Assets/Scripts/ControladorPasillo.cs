using UnityEngine;

public class ControladorPasillo : MonoBehaviour {
    public GameObject sirvienteConPostre; // Arrastra aquí a la sirviente de la jerarquía

    void Start() {
        // Al entrar al pasillo, comprobamos si el cupcake ya fue envenenado
        if (GameManager.Instance != null && GameManager.Instance.cupcakeEnvenenado) {
            sirvienteConPostre.SetActive(true); // Aparece la sirviente lista para el accidente
        }
        else {
            sirvienteConPostre.SetActive(false); // Sigue escondida/desactivada
        }
    }
}
