using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

    //variables de movimiento
    public static float movementSpeed = 1.5f;
    public InputSystem_Actions inputActions;

    private Rigidbody2D rbody;
    private Vector2 moveInput;

    // variables de interacción
    [Header("Configuración de Interacción")]
    public float distanciaMaximaInteraccion = 3f;

    private void Awake() {
        // Inicialización de Rigidbody y persistencia del objeto.
        DontDestroyOnLoad(this.gameObject);
        rbody = GetComponent<Rigidbody2D>();

        // Inicialización del Input System.
        if (inputActions == null) {
            inputActions = new InputSystem_Actions();
            inputActions.Player.Enable();
        }
    }

    private void OnEnable() {
        if (inputActions != null) {

            inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
            inputActions.Player.Move.Enable();

            var mouseAction = inputActions.FindAction("Left Button [Mouse]");


            if (mouseAction != null) {
                mouseAction.performed += OnInteractPerformed;
                mouseAction.Enable();
            }
        }
    }

    private void OnDisable() {
        if (inputActions != null) {
            // Limpieza y deshabilitación de acciones.
            inputActions.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled -= ctx => moveInput = Vector2.zero;
            inputActions.Player.Move.Disable();

            var mouseAction = inputActions.FindAction("Left Button [Mouse]");

            if (mouseAction != null) {
                mouseAction.performed -= OnInteractPerformed;
                mouseAction.Disable();
            }
        }
    }

    void FixedUpdate() {
        // Mueve el Rigidbody 2D basado en el input.
        Vector2 currentPos = rbody.position;
        Vector2 inputVector = Vector2.ClampMagnitude(moveInput, 1);
        Vector2 movement = inputVector * movementSpeed * Time.fixedDeltaTime;
        Vector2 newPos = currentPos + movement;

        rbody.MovePosition(newPos);
    }

    // Logica de interacción

    private void OnInteractPerformed(InputAction.CallbackContext context) {

        Debug.Log("Input Detectado: Acción de Interacción Ejecutada.");

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        ItemData itemSeleccionado = InventarioCursor.Instance.GetItemSeleccionado();

        // Conversión de posición de pantalla a mundo (necesario para Raycast 2D).
        Vector2 puntoMundo = Camera.main.ScreenToWorldPoint(mousePosition);

        // Raycast 2D para detectar Colliders 2D en el mundo.
        RaycastHit2D hit2D = Physics2D.Raycast(puntoMundo, Vector2.zero, 0f);

        if (hit2D.collider != null) {
            // Si golpea algo, intenta usar el ítem.
            ManejarUsoDeItem(hit2D.collider.gameObject, itemSeleccionado);
        }
        else {
            // Deseleccionar si el clic falla y teníamos un ítem activo.
            if (itemSeleccionado != null && Inventario.Instance != null) {
                Inventario.Instance.DeselectItem();
            }
        }
    }

    private void ManejarUsoDeItem(GameObject objetoGolpeado, ItemData item) {

        ObjetoInteractuable objetivo = objetoGolpeado.GetComponent<ObjetoInteractuable>();

        if (objetivo != null) {
            // Llama a la lógica de la receta del objeto interactuable.
            objetivo.UsarItemEnObjeto(item);
        }
        else {
            // Limpia el cursor si el objeto golpeado no tenía el script ObjetoInteractuable.
            if (item != null && Inventario.Instance != null) {
                Inventario.Instance.DeselectItem();
            }
        }
    }
}