using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : MonoBehaviour
{
    private Vector2 moveInput;
    public Vector2 mousePosition;
    public Animator playerAnimator;
    public InputActionReference playerMovementAction;
    public InputActionReference playerFireAction;
    public InputActionReference playerRotationAction;

    private bool isFireButtonPressed;
    bool isFireAnimationAlreadyRunning;

    public bool IsFireButtonPressed
    {
        get { return isFireButtonPressed; }
        set { isFireButtonPressed = value; }
    }

    private void OnEnable()
    {
        playerFireAction.action.performed += ctx => SetFiringButtonStatus(ctx);
        playerFireAction.action.canceled += ctx => SetFiringButtonStatus(ctx);
    }

    private void OnDisable()
    {
        playerFireAction.action.performed -= ctx => SetFiringButtonStatus(ctx);
        playerFireAction.action.canceled -= ctx => SetFiringButtonStatus(ctx);
    }

    void Update()
    {
        // Read movement input
        moveInput = playerMovementAction.action.ReadValue<Vector2>();
        EventManager.latestMovementInput?.Invoke(moveInput);

        // Read mouse position
        mousePosition = playerRotationAction.action.ReadValue<Vector2>();
        EventManager.latestMousePosition?.Invoke(mousePosition);
    }

    #region Fire Input Specific methods

    void SetFiringButtonStatus(InputAction.CallbackContext ctx)
    {
        IsFireButtonPressed = ctx.ReadValueAsButton();

        if (IsFireButtonPressed)
            EventManager.WeaponFireStarted?.Invoke();
        else
            EventManager.WeaponFireStopped?.Invoke();
    }


    #endregion

    #region Visual appearance methods

    public void SetPlayerFireStatus(bool isFiring)
    {
        if (isFiring && !isFireAnimationAlreadyRunning)
        {
            isFireAnimationAlreadyRunning = true;
            Debug.Log("Player is shooting");

            // use playerAnimator to enable Animamtion here if any
        }
        else if (!isFiring && isFireAnimationAlreadyRunning)
        {
            isFireAnimationAlreadyRunning = false;
            Debug.Log("Player is not shooting");

            // use playerAnimator to enable Animamtion here if any
        }
    }


    #endregion
}
