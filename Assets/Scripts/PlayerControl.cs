using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private static PlayerControl instance;

    public static float movementSpeed = 1.5f;
    public InputSystem_Actions inputActions;

    private Rigidbody2D rbody;
    private Vector2 moveInput;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

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
            inputActions.Player.Move.Disable();
        }
    }

    void FixedUpdate() {
        if (Inventario.Instance != null && Inventario.Instance.EstaBloqueadoElMovimiento) {
            return;
        }

        Vector2 inputVector = Vector2.ClampMagnitude(moveInput, 1);

        if (inputVector.sqrMagnitude < 0.01f) return;

        Vector2 isoVector = new Vector2(
            inputVector.x - inputVector.y,
            (inputVector.x + inputVector.y) / 2f
        );

        Vector2 movement = isoVector * movementSpeed * Time.fixedDeltaTime;
        rbody.MovePosition(rbody.position + movement);
    }
}