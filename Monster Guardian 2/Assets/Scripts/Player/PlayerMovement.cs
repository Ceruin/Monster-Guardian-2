using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 500f;
    public float rotationSpeed = 720.0f;
    public float jumpForce = 5f;
    private Rigidbody body;
    private Vector2 _moveInput;
    private float verticalVelocity = 0.0f;
    private bool jumpRequest = false;
    private PlayerControls actions;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        actions = new PlayerControls();

        actions.Player.Movement.performed += OnMove_performed;
        actions.Player.Movement.canceled += OnMove_canceled;

        actions.Player.Jump.performed += OnJump_performed;
    }

    private void OnEnable()
    {
        // Enable the input actions when the object is enabled
        actions.Player.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions when the object is disabled
        actions.Player.Disable();
    }

    public void OnMove_performed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnMove_canceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }

    public void OnJump_performed(InputAction.CallbackContext context)
    {
        // Set a flag to indicate that a jump has been requested
        jumpRequest = true;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Convert the movement input into the camera's coordinate space
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Ignore the y component of the camera's forward vector
        cameraForward.y = 0;

        // Normalize the vectors (important if the camera isn't level)
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = (cameraForward * _moveInput.y + cameraRight * _moveInput.x) * moveSpeed * Time.deltaTime;

        HandleJump();

        // Combine the horizontal and vertical movement
        Vector3 finalMovement = movement + new Vector3(0, verticalVelocity, 0) * Time.deltaTime;

        HandleRotation(movement);
        
        body.velocity = finalMovement;
    }

    private void HandleRotation(Vector3 movement)
    {
        // If there's some horizontal movement, rotate the character to face the move direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleJump()
    {
                // Apply gravity
                if (true) // if grounded
                {
                    verticalVelocity = 0;  // Reset the vertical velocity if the character is grounded

                    // If a jump has been requested, apply an upward force
                    if (jumpRequest)
                    {
                        verticalVelocity = jumpForce;
                        jumpRequest = false;  // Reset the jump request
                    }
                }
                else
                {
                    verticalVelocity += Physics.gravity.y * Time.deltaTime;  // Apply gravity
                }
    }
}