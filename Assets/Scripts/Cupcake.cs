using UnityEngine;

public class Cupcake : MonoBehaviour {

    public Color newColor = Color.darkGreen;  // El color al que cambiará

    private SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();  // Obtiene el sprite
    }

    private void OnMouseDown()  // Detecta clic en el objeto
    {
        sr.color = newColor;
    }
}
