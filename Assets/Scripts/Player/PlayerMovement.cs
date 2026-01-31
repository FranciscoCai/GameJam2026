using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float acceleration = 8f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float maxForwardSpeed = 12f;
    [SerializeField] private float maxBackwardSpeed = 6f;
    [SerializeField] private float rotationSpeedFactor = 0.5f; // cuánto reduce la velocidad al girar

    [Header("Rotation")]
    [SerializeField] private float rotationAcceleration = 180f; // grados/s²
    [SerializeField] private float rotationDeceleration = 360f; // grados/s²
    [SerializeField] private float maxRotationSpeed = 180f; // grados/s

    private InputSystem_Actions inputActions;
    private Rigidbody rb;

    private Vector2 moveInput;

    private float currentSpeed;         // velocidad forward/back
    private float currentRotationSpeed; // velocidad angular Y

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation()
    {
        float turnInput = moveInput.x;

        if (Mathf.Abs(turnInput) > 0.01f)
        {
            currentRotationSpeed += turnInput * rotationAcceleration * Time.fixedDeltaTime;
        }
        else
        {
            currentRotationSpeed = Mathf.MoveTowards(currentRotationSpeed,0f,rotationDeceleration * Time.fixedDeltaTime);
        }

        currentRotationSpeed = Mathf.Clamp(currentRotationSpeed,-maxRotationSpeed,maxRotationSpeed);

        if (Mathf.Abs(currentRotationSpeed) > 0.01f)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, currentRotationSpeed * Time.fixedDeltaTime, 0f);rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }

    private void HandleMovement()
    {
        float forwardInput = moveInput.y;

        float speedModifier = 1f - Mathf.Min(Mathf.Abs(currentRotationSpeed) / maxRotationSpeed, 1f) * rotationSpeedFactor;

        if (Mathf.Abs(forwardInput) > 0.01f)
        {
            currentSpeed += forwardInput * acceleration * Time.fixedDeltaTime;
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed,0f,deceleration * Time.fixedDeltaTime);
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxBackwardSpeed, maxForwardSpeed);

        float finalSpeed = currentSpeed * speedModifier;

        Vector3 velocity = rb.linearVelocity;
        Vector3 forwardVelocity = transform.forward * finalSpeed;

        rb.linearVelocity = new Vector3(forwardVelocity.x,velocity.y,forwardVelocity.z);
    }
}
