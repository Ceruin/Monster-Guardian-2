using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private Vector2 _cameraInput;
    private CinemachineFreeLook _virtualCamera;

    private void Awake()
    {
        PlayerControls actions = new PlayerControls();
        _virtualCamera = GetComponent<CinemachineFreeLook>();
        _virtualCamera.m_XAxis.m_MaxSpeed = 0; // Disable horizontal movement initially
        _virtualCamera.m_YAxis.m_MaxSpeed = 0; // Disable vertical movement initially
        actions.Player.RightClick.performed += RightClick_performed;
        actions.Player.RightClick.canceled += RightClick_canceled;
        actions.Player.CameraRotate.performed += CameraRotate_performed;
        actions.Player.CameraRotate.canceled += CameraRotate_canceled;

        actions.Enable();

        Cursor.lockState = CursorLockMode.None;
    }

    private void RightClick_canceled(InputAction.CallbackContext obj)
    {
        _virtualCamera.m_XAxis.m_MaxSpeed = 0; // Disable horizontal movement when the button is released
        _virtualCamera.m_YAxis.m_MaxSpeed = 0; // Disable vertical movement when the button is released
    }

    private void RightClick_performed(InputAction.CallbackContext obj)
    {
        _virtualCamera.m_XAxis.m_MaxSpeed = 300; // Enable horizontal movement when the button is pressed
        _virtualCamera.m_YAxis.m_MaxSpeed = 2; // Enable vertical movement when the button is pressed
    }

    private void CameraRotate_canceled(InputAction.CallbackContext context)
    {
        _cameraInput = Vector2.zero;
    }

    private void CameraRotate_performed(InputAction.CallbackContext context)
    {
        _cameraInput = context.ReadValue<Vector2>();

        // Pass the input to the Cinemachine camera
        _virtualCamera.m_XAxis.Value += _cameraInput.x;
        _virtualCamera.m_YAxis.Value += _cameraInput.y;  // remove the minus sign to invert the vertical rotation
    }
}