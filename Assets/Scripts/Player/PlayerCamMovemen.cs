using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamMovemen : MonoBehaviour
{
    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 2.5f;
    [SerializeField] private float gamepadSensitivity = 120f;
    [SerializeField] private Vector2 verticalClamp = new Vector2(-80f, 80f);

    private InputSystem_Actions inputActions;
    private PlayerInput playerInput;

    private Vector2 lookInput;
    private float xRotation;
    private float yRotation;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        playerInput = GetComponentInParent<PlayerInput>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Look.performed += OnLook;
        inputActions.Player.Look.canceled += OnLook;
    }

    private void OnDisable()
    {
        inputActions.Player.Look.performed -= OnLook;
        inputActions.Player.Look.canceled -= OnLook;
        inputActions.Player.Disable();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        float sensitivity = GetCurrentSensitivity();
        RotateCamera(sensitivity);
    }

    private float GetCurrentSensitivity()
    {
        return playerInput.currentControlScheme switch
        {
            "Gamepad" => gamepadSensitivity * Time.deltaTime,
            _ => mouseSensitivity
        };
    }

    private void RotateCamera(float sensitivity)
    {
        Vector2 delta = lookInput * sensitivity;

        xRotation -= delta.y;
        xRotation = Mathf.Clamp(xRotation, verticalClamp.x, verticalClamp.y);

        yRotation += delta.x;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
