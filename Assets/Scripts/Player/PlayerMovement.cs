using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float acceleration = 8f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float maxForwardSpeed = 12f;
    [SerializeField] private float maxBackwardSpeed = 6f;

    [Header("Rotation")]
    [SerializeField] private float baseTurnSpeed = 120f;
    [SerializeField] private float turnSpeedAtMaxVelocity = 40f;

    private InputSystem_Actions inputActions;
    private CharacterController characterController;

    private Vector2 moveInput;
    private float currentSpeed;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();
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

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        float forwardInput = moveInput.y;

        if (Mathf.Abs(forwardInput) > 0.01f)
        {
            currentSpeed -= forwardInput * acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        currentSpeed = Mathf.Clamp(currentSpeed,-maxBackwardSpeed,maxForwardSpeed);

        Vector3 forwardMovement = transform.forward * currentSpeed;
        characterController.Move(forwardMovement * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float turnInput = moveInput.x;
        float speedPercent = Mathf.Abs(currentSpeed) / maxForwardSpeed;
        float turnSpeed = Mathf.Lerp(baseTurnSpeed,turnSpeedAtMaxVelocity,speedPercent);

        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
    }
}
