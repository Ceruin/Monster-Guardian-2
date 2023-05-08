using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 5.0f;
    public float smoothTime = 0.1f;

    private Vector2 _cameraInput;
    private CinemachineVirtualCamera _virtualCamera;
    private Vector3 _currentVelocity;

    private void Awake()
    {
        PlayerControls actions = new PlayerControls();

        actions.Movement.CameraRotate.performed += CameraRotate_performed;
        actions.Movement.CameraRotate.canceled += CameraRotate_canceled;

        actions.Enable();

        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void CameraRotate_canceled(InputAction.CallbackContext context)
    {
        _cameraInput = Vector2.zero;
    }

    private void CameraRotate_performed(InputAction.CallbackContext context)
    {
        _cameraInput = context.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //if (_cameraInput != Vector2.zero)
        //{
        //    float rotationY = _cameraInput.x * rotationSpeed * Time.deltaTime;
        //    float rotationX = _cameraInput.y * rotationSpeed * Time.deltaTime;

        //    Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        //    _virtualCamera.transform.rotation = Quaternion.Lerp(_virtualCamera.transform.rotation, targetRotation * _virtualCamera.transform.rotation, smoothTime);
        //}
    }

    private void FixedUpdate()
    {
    }
}