using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerModel playerModel;
    public PlayerView playerView;

    private Vector2 moveInput;
    private Vector2 mousePosition;
    private bool isPlayerShooting;

    private CharacterController characterController;

    public Camera mainCamera;
    public Transform player;

    public WeaponController weaponController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        EventManager.latestMovementInput += ReceiveLatestMoveInput;
        EventManager.latestMousePosition += ReceiveLatestMousePositionInput;
        EventManager.WeaponFireStarted += EnablePlayerShootingStatus;
        EventManager.WeaponFireStopped += DisablePlayerShootingStatus;

        EventManager.playerKilled += OnPlayerKilled;
    }

    private void OnDisable()
    {
        EventManager.latestMovementInput -= ReceiveLatestMoveInput;
        EventManager.latestMousePosition -= ReceiveLatestMousePositionInput;
        EventManager.WeaponFireStarted -= EnablePlayerShootingStatus;
        EventManager.WeaponFireStopped -= DisablePlayerShootingStatus;

        EventManager.playerKilled -= OnPlayerKilled;
    }

    private void Update()
    {
        ControlPlayerMovement();
        ControlPlayerRotation();
        ControlPlayerShoot();
    }

    #region Movement rotation and shoot methods

    void ControlPlayerMovement()
    {
        if (moveInput != Vector2.zero)
        {
            Vector3 moveVector = new Vector3(moveInput.x, 0, moveInput.y);
            characterController.Move(moveVector * playerModel.moveSpeed * Time.deltaTime);
        }
    }


    void ControlPlayerRotation()
    {
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 direction = (hit.point - transform.position).normalized;
            direction.y = 0; // Ensure rotation only on the Y axis

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                player.rotation = Quaternion.Lerp(transform.rotation, targetRotation, playerModel.rotateSpeed * Time.deltaTime);
            }
        }
    }

    void ControlPlayerShoot()
    {
        // isPlayerShooting = playerView.IsPlayerShooting();
        playerView.SetPlayerFireStatus(isPlayerShooting);
    }

    #endregion

    #region Event Callback methods

    void ReceiveLatestMoveInput(Vector2 _moveInput)
    {
        // Receive movement input
        moveInput = _moveInput;
    }

    void ReceiveLatestMousePositionInput(Vector2 _mousePositionInput)
    {
        // Receive mouse position input
        mousePosition = _mousePositionInput;
    }

    void EnablePlayerShootingStatus()
    {
        isPlayerShooting = true;
        weaponController.StartFiring();
    }

    void DisablePlayerShootingStatus()
    {
        isPlayerShooting = false;
        weaponController.StopFiring();
    }

    #endregion

    void OnPlayerKilled()
    {
        EventManager.GameEnded?.Invoke();
        gameObject.SetActive(false);
    }
}
