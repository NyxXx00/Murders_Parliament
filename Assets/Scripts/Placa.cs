using UnityEngine;

public class Placa : MonoBehaviour
{
    void OnMouseDown() {
        float dist = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);
        if (dist < 3f) {
            Debug.Log("Placa: Basil 'Black Crow' Corvino - Fundado en 1983");
            // Aquí puedes activar un texto flotante en tu UI si tienes
        }
    }
}
