using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private Vector2 _cameraInput;
    private bool isAiming = false;

    private void Awake()
    {
        PlayerControls actions = new PlayerControls();

        actions.Player.CameraRotate.performed += CameraRotate_performed;
        actions.Player.CameraRotate.canceled += CameraRotate_canceled;

        actions.Enable();

        Cursor.lockState = CursorLockMode.None;
    }

    private void CameraRotate_canceled(InputAction.CallbackContext context)
    {
        _cameraInput = Vector2.zero;
    }

    private void CameraRotate_performed(InputAction.CallbackContext context)
    {
        _cameraInput = context.ReadValue<Vector2>();
    }
}