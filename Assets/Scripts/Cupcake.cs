using UnityEngine;

public class Cupcake : MonoBehaviour {
    public Color newColor = Color.green;  // Color al que cambiará


    private SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() {

        // Cambia de color
        sr.color = newColor;

    }
}
