using UnityEngine;

public class PiedraRecompensa : MonoBehaviour {
    public PuzzleMosaico scriptMosaico; // Arrastra el objeto que tiene el script del mosaico

    void OnMouseDown() {
        // Llama a la función que ya escribimos en tu script
        scriptMosaico.RecogerPiedra();

        // Destruye la piedra del suelo porque ya la "tienes"
        Destroy(gameObject);
    }
}