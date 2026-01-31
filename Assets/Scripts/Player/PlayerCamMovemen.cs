using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamMovemen : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] private float mouseSensitivity = 2.5f;
    [SerializeField] private float gamepadSensitivity = 120f;

    [Header("Free Look Limits")]
    [SerializeField] private Vector2 verticalClamp = new Vector2(-60f, 60f);
    [SerializeField] private Vector2 horizontalClamp = new Vector2(-90f, 90f);

    [Header("Gamepad Return")]
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private float deadZone = 0.05f;

    private PlayerInput playerInput;

    private InputSystem_Actions inputActions;
    private Vector2 lookInput;
    private Vector2 lookOffset;

    private void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        inputActions = new InputSystem_Actions();
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
        HandleLook();
        ApplyRotation();
    }

    private void HandleLook()
    {
        bool isGamepad = IsUsingGamepad();
        bool hasInput = lookInput.sqrMagnitude > deadZone * deadZone;

        float sensitivity = GetSensitivity();
        Vector2 delta = lookInput * sensitivity;

        if (hasInput)
        {
            lookOffset.x += delta.x;
            lookOffset.y -= delta.y;
        }
        else if (isGamepad)
        {
            lookOffset = Vector2.Lerp(lookOffset,Vector2.zero,returnSpeed * Time.deltaTime);
        }

        lookOffset.x = Mathf.Clamp(lookOffset.x, horizontalClamp.x, horizontalClamp.y);
        lookOffset.y = Mathf.Clamp(lookOffset.y, verticalClamp.x, verticalClamp.y);
    }

    private void ApplyRotation()
    {
        transform.localRotation = Quaternion.Euler(lookOffset.y,lookOffset.x,0f);
    }

    private float GetSensitivity()
    {
        if (playerInput == null) return mouseSensitivity;

        return playerInput.currentControlScheme switch
        {
            "Gamepad" => gamepadSensitivity * Time.deltaTime,_ => mouseSensitivity};
        }

    private bool IsUsingGamepad()
    {
        return playerInput != null &&playerInput.currentControlScheme == "Gamepad";
    }
}
