using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 720.0f;
    public float jumpForce = 5f;
    private CharacterController controller;
    private Vector2 _moveInput;
    private float verticalVelocity = 0.0f;
    private bool jumpRequest = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (controller.isGrounded && context.started)
        {
            jumpRequest = true;
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = (cameraForward * _moveInput.y + cameraRight * _moveInput.x) * moveSpeed * Time.deltaTime;

        if (controller.isGrounded)
        {
            verticalVelocity = 0;

            if (jumpRequest)
            {
                verticalVelocity = jumpForce;
                jumpRequest = false;
            }
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        Vector3 finalMovement = movement + new Vector3(0, verticalVelocity, 0) * Time.deltaTime;

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        controller.Move(finalMovement);
    }
}
