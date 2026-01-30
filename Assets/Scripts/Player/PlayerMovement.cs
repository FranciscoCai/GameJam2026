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
    [SerializeField] private float turnAcceleration = 300f;
    [SerializeField] private float turnDeceleration = 400f;
    [SerializeField] private float maxTurnSpeed = 140f; 
    [SerializeField] private float maxTurnSpeedAtMaxVelocity = 60f;

    private InputSystem_Actions inputActions;
    private CharacterController characterController;

    private Vector2 moveInput;

    private float currentSpeed;
    private float currentTurnSpeed;

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
        float turnInput = moveInput.x;

        bool isTurning = Mathf.Abs(turnInput) > 0.5f;

        if (Mathf.Abs(forwardInput) > 0.01f)
        {
            currentSpeed += forwardInput * acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed,0f,deceleration *  Time.deltaTime);
        }

        currentSpeed = Mathf.Clamp(currentSpeed,-maxBackwardSpeed,maxForwardSpeed);

        Vector3 move = transform.forward * currentSpeed * -1;
        characterController.Move(move * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float turnInput = moveInput.x;

        float speedPercent = Mathf.Abs(currentSpeed) / maxForwardSpeed;

        float effectiveMaxTurnSpeed = Mathf.Lerp(maxTurnSpeed,maxTurnSpeedAtMaxVelocity,speedPercent);

        if (Mathf.Abs(turnInput) > 0.01f)
        {
            currentTurnSpeed +=turnInput *turnAcceleration *(1f - speedPercent) *Time.deltaTime;
        }
        else
        {
            currentTurnSpeed = Mathf.MoveTowards(currentTurnSpeed,0f,turnDeceleration * Time.deltaTime);
        }

        currentTurnSpeed = Mathf.Clamp(currentTurnSpeed,-effectiveMaxTurnSpeed,effectiveMaxTurnSpeed);

        transform.Rotate(Vector3.up * currentTurnSpeed * Time.deltaTime);
    }
}
