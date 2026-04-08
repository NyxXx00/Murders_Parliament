using UnityEngine;

public class Sabotaje : MonoBehaviour {
    public GameObject manchaAceiteVisual; // El sprite de la mancha en el suelo
    public ItemData objetoAceite;         // Arrastra aquí el ItemData del Aceite desde tu carpeta de Assets

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject) {
                // PRIMERO: Comprobar si tenemos algo seleccionado en el inventario
                if (Inventario.Instance.DatosItemSeleccionado != null) {

                    // SEGUNDO: Comprobar si eso que tenemos seleccionado es el ACEITE
                    if (Inventario.Instance.DatosItemSeleccionado == objetoAceite) {
                        PonerMancha();
                    }
                    else {
                        Debug.Log("Este objeto no sirve para sabotear el suelo.");
                    }
                }
                else {
                    Debug.Log("No tienes nada seleccionado para usar aquí.");
                }
            }
        }
    }

    void PonerMancha() {
        // Activamos la mancha visual
        manchaAceiteVisual.SetActive(true);

        // Avisamos al GameManager que el pasillo está listo
        if (GameManager.Instance != null) {
            GameManager.Instance.pasilloSaboteado = true;
            // Opcional: Si quieres marcar el tieneAceite como true aquí también por seguridad:
            GameManager.Instance.tieneAceite = true;
        }

        // CONSUMIR EL ACEITE (Igual que en el Cupcake)
        Inventario.Instance.RemoveItem(objetoAceite.ItemID);
        Inventario.Instance.DeselectItem();

        Debug.Log("¡Aceite colocado y eliminado del inventario!");
    }
}
