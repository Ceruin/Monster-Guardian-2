using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 200f;

    private Rigidbody _rigidbody;
    private Vector2 _moveInput;
    private Vector2 _rotationInput;
    private CinemachineFreeLook _camera;
    private bool _jumpRequested;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        PlayerControls actions = new PlayerControls();

        actions.Movement.Move.performed += OnMove_performed;
        actions.Movement.Move.canceled += OnMove_canceled;

        actions.Movement.CameraRotate.performed += OnRotate_performed;
        actions.Movement.CameraRotate.canceled += OnRotate_canceled;

        actions.Movement.Jump.performed += OnJump;

        actions.Enable();
    }

    private void Update()
    {
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    public void OnMove_performed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnMove_canceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }

    public void OnRotate_performed(InputAction.CallbackContext context)
    {
        _rotationInput = context.ReadValue<Vector2>();
    }

    public void OnRotate_canceled(InputAction.CallbackContext context)
    {
        _rotationInput = Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _jumpRequested = true;
        }
    }

    private void HandleMovement()
    {
        // Use the camera's forward and right vectors as the basis for movement
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Ignore the y component of the forward and right vectors to avoid vertical movement
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Calculate the desired move direction based on input and the camera's orientation
        Vector3 moveDirection = forward * _moveInput.y + right * _moveInput.x;
        moveDirection.Normalize();

        _rigidbody.velocity = new Vector3(moveDirection.x * moveSpeed, _rigidbody.velocity.y, moveDirection.z * moveSpeed);
    }

    private void HandleRotation()
    {
        float rotationY = _rotationInput.x * rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotationY, 0, Space.World);
    }

    private void HandleJump()
    {
        if (_jumpRequested)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _jumpRequested = false;
        }
    }
}