using UnityEngine;
using UnityEngine.InputSystem;

public class Mascarilla : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float fallSpeed = 50f;
    [SerializeField] private float returnSpeed = 300f;

    private PlayerInput playerInput;

    [Header("Scripts to Disable")]
    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] private PlayerDash dashScript;

    private InputSystem_Actions inputActions;
    private RectTransform rectTransform;

    private Vector2 startPosition;
    private bool isReturning;

    private void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        rectTransform = GetComponent<RectTransform>();
        inputActions = new InputSystem_Actions();
    }

    private void Start()
    {
        startPosition = rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        if (isReturning)
        {
            ReturnToStart();
        }
        else
        {
            FallDown();
        }
    }

    private void FallDown()
    {
        if (rectTransform.anchoredPosition.y! <= -875) return;
        rectTransform.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;
    }

    private void ReturnToStart()
    {
        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition,startPosition,returnSpeed * Time.deltaTime);

        if (Vector2.Distance(rectTransform.anchoredPosition, startPosition) < 0.1f)
        {
            rectTransform.anchoredPosition = startPosition;FinishReturn();
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (isReturning) return;

        isReturning = true;

        if (movementScript != null)movementScript.enabled = false;

        if (dashScript != null)dashScript.enabled = false;
    }

    private void FinishReturn()
    {
        isReturning = false;

        if (movementScript != null)movementScript.enabled = true;

        if (dashScript != null)dashScript.enabled = true;
    }
}
