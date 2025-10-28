using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItem : MonoBehaviour {
    [Header("Configuraci�n")] // Deber�a aparecer en el Inspector
    public Item item; // Asigna tu Item aqu� en el Inspector
    public Inventario inventory; // Referencia al Inventario
    public float pickupRange = 2f;
    public LayerMask pickupLayer = 1 << 6; // Capa del jugador o interactuable
    public CinemachineVirtualCamera virtualCamera; // Tipo est�ndar para Cinemachine 3.1.5
    public InputActionAsset inputActions; // Asigna tu Input Action Asset (ej. PlayerControls)

    [Header("Efectos")] // Deber�a aparecer en el Inspector
    public AudioSource pickupSound;
    public ParticleSystem pickupParticles;

    private Camera cam; // C�mara f�sica asociada
    private InputAction pickupAction;

    public void Start() {
        Debug.Log("Start: Iniciando asignaci�n de c�mara.");
        // Usa Camera.main como fallback principal
        cam = Camera.main;
        if (cam != null) {
            Debug.Log("C�mara asignada desde Camera.main: " + cam.name);
        }
        else {
            Debug.LogError("No se encontr� Camera.main. Aseg�rate de tener una c�mara con la etiqueta 'MainCamera'.");
        }

        // Verifica referencias
        if (item == null || inventory == null || inputActions == null) {
            Debug.LogError("Faltan asignaciones en el Inspector: 'item', 'inventory' o 'inputActions'.");
            return;
        }
        Debug.Log("Referencias verificadas: item=" + (item != null) + ", inventory=" + (inventory != null) + ", inputActions=" + (inputActions != null));

        // Configura la acci�n de pickup
        try {
            pickupAction = inputActions.FindActionMap("Player")?.FindAction("Pickup");
            if (pickupAction != null) {
                pickupAction.performed += ctx => OnPickup();
                pickupAction.Enable();
                Debug.Log("Acci�n 'Pickup' configurada correctamente.");
            }
            else {
                Debug.LogError("Acci�n 'Pickup' no encontrada en el Input Action Asset. Verifica el nombre del Action Map.");
            }
        }
        catch (System.Exception e) {
            Debug.LogError("Error al configurar la acci�n de entrada: " + e.Message);
        }
    }

    public void OnDestroy() {
        if (pickupAction != null) {
            pickupAction.Disable();
        }
    }

    public void OnPickup() {
        Debug.Log("Clic detectado en OnPickup. C�mara actual: " + (cam != null ? cam.name : "null"));
        if (cam != null) {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.Log("Raycast desde: " + ray.origin + " hacia: " + ray.direction);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, pickupRange, pickupLayer);

            if (hit.collider != null) {
                Debug.Log("Colisi�n detectada con: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject == gameObject) {
                    Debug.Log("Colisi�n con este objeto (" + gameObject.name + ").");
                    if (inventory.AddItem(item, 1)) {
                        Debug.Log("�Recogido: " + item.itemName + "!");
                        Destroy(gameObject);
                    }
                    else {
                        Debug.Log("Inventario lleno.");
                    }
                }
                else {
                    Debug.Log("Colisi�n con otro objeto, no con este.");
                }
            }
            else {
                Debug.Log("No se detect� colisi�n en el raycast.");
            }
        }
        else {
            Debug.LogError("C�mara no asignada en OnPickup. Aseg�rate de tener una c�mara con la etiqueta 'MainCamera'.");
        }
    }
}