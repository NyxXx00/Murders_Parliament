using UnityEngine;

public class PlayerMov : MonoBehaviour {
    public static float movementSpeed = 1.5f;
    public InputSystem_Actions inputActions;

    private Rigidbody2D rbody;
    // Almacena la entrada del jugador
    private Vector2 moveInput;

    private void Awake() {
        rbody = GetComponent<Rigidbody2D>();
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
        }
    }

    private void OnDisable() {
        if (inputActions != null) {
            // Deshabilita la acción cuando el objeto se deshabilita
            inputActions.Player.Move.Disable();
        }
    }

    void FixedUpdate() {
        Vector2 currentPos = rbody.position;
        Vector2 inputVector = Vector2.ClampMagnitude(moveInput, 1);
        Vector2 movement = inputVector * movementSpeed * Time.fixedDeltaTime;
        Vector2 newPos = currentPos + movement;

        rbody.MovePosition(newPos);
    }
}
