using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OpenCloseItemRequiered : MonoBehaviour
{
    [SerializeField] private RectTransform targetImage;
    [SerializeField] private Vector2 visiblePosition;
    [SerializeField] private Vector2 hiddenPosition;
    [SerializeField] private float moveDuration = 0.3f; // Duraci¨®n del movimiento en segundos

    private bool isVisible = false;
    private InputSystem_Actions inputActions;
    private Coroutine moveCoroutine;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.ItemMenu.performed += ctx => ToggleImage();
        if (targetImage != null)
            targetImage.anchoredPosition = hiddenPosition; // Empieza oculto
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void OnDestroy()
    {
        inputActions.Player.ItemMenu.performed -= ctx => ToggleImage();
        inputActions.Dispose();
    }

    private void ToggleImage()
    {
        isVisible = !isVisible;
        Vector2 targetPos = isVisible ? visiblePosition : hiddenPosition;
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveImage(targetPos));
    }

    private IEnumerator MoveImage(Vector2 targetPos)
    {
        Vector2 startPos = targetImage.anchoredPosition;
        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            targetImage.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }
        targetImage.anchoredPosition = targetPos;
    }
}
