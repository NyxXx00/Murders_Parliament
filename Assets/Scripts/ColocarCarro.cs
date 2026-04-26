using UnityEngine;

public class ColocarEnCarro : MonoBehaviour {

    public ItemData objetoNecesario;
    public Transform puntoDondeDejar;
    public GameObject objetoVisualEnCarro;


    [Header("Misiones")]
    public int taskId;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // Si clicamos en este carro
            if (hit.collider != null && hit.collider.gameObject == gameObject) {

                // Comprobamos si tenemos el objeto en la mano
                if (Inventario.Instance.DatosItemSeleccionado == objetoNecesario) {
                    DejarObjeto();
                }
                else {
                    Debug.Log("No puedes dejar eso aquí.");
                }
            }
        }
    }

    void DejarObjeto() {
        Debug.Log("Objeto colocado en el carro.");

        if (objetoVisualEnCarro != null) {
            objetoVisualEnCarro.SetActive(true);

            objetoVisualEnCarro.transform.SetParent(this.transform);

            if (puntoDondeDejar != null) {
                objetoVisualEnCarro.transform.position = puntoDondeDejar.position;
            }
        }
        if (TareasManager.Instance != null) {
            TareasManager.Instance.CompleteTask(taskId);
            Debug.Log("Tarea completada: " + taskId);
        }

        Inventario.Instance.RemoveItem(objetoNecesario.ItemID);
        Inventario.Instance.DeselectItem();

    }
}