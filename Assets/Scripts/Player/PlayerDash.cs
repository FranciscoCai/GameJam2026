using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashMaxSpeed = 25f;
    [SerializeField] private float dashMinSpeed = 5f;
    [SerializeField] private float dashDeceleration = 60f;

    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private VfxCollision vfx;
    [SerializeField] private SpeedController vfxV;

    private InputSystem_Actions inputActions;
    private Rigidbody rb;

    private bool isDashing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Attack.performed += OnDash;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnDash;
        inputActions.Player.Disable();
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if (isDashing)
            return;
        StartCoroutine(vfxV.actionLines());
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        isDashing = true;

        if (playerMovement != null)playerMovement.enabled = false;
        if (vfx != null) vfx.enabled = true;

        float currentSpeed = dashMaxSpeed;

        while (currentSpeed > dashMinSpeed)
        {
            Vector3 dashVelocity = transform.forward * currentSpeed;

            rb.linearVelocity = new Vector3(dashVelocity.x,rb.linearVelocity.y,dashVelocity.z);

            currentSpeed -= dashDeceleration * Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        isDashing = false;
        if (playerMovement != null)playerMovement.enabled = true;
        if (vfx != null) vfx.enabled = false;
    }
}
