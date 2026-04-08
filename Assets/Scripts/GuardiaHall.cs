using UnityEngine;

public class GuardiaHall : MonoBehaviour {

    [Header("Configuración de paso")]
    [SerializeField] private GameObject puerta;

    void Start() {
        // Al cargar la escena, verificamos el estado del jarrón
        if (GameManager.Instance != null && GameManager.Instance.jarronRoto) {
            // 1. Activamos la puerta antes de que el guardia se desactive a sí mismo
            if (puerta != null) {
                puerta.SetActive(true);
                Debug.Log("Puerta activada: El camino está libre.");
            }

            // 2. El guardia se desactiva
            Debug.Log("Guardia del Hall desactivado porque el jarrón ya se rompió.");
            gameObject.SetActive(false);
        }
        else {
            // Si el guardia está presente, nos aseguramos de que la puerta esté cerrada
            if (puerta != null) {
                puerta.SetActive(false);
            }
        }
    }
}
