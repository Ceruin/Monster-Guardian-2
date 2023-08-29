using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCamera : MonoBehaviour
{
    public Transform target; // Target to follow (usually the player)
    public float distance = 10.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    public Vector2 sensitivity = new Vector2(0.01f, 0.01f);
    public int verticalPitch = 85;
    private PlayerControls actions;
    private Vector3 originalOffset;
    private Vector2 _cameraInput;
    private float yaw = 0.0f;

    private void Awake()
    {
        actions = new PlayerControls();
        actions.Player.CameraRotate.performed += CameraRotate_performed;
        actions.Player.CameraRotate.canceled += CameraRotate_canceled;
        actions.Enable();

        Cursor.lockState = CursorLockMode.Confined;

        originalOffset = new Vector3(0f, height, -distance);

        yaw = Mathf.Atan2(originalOffset.x, originalOffset.z);
    }

    void LateUpdate()
    {
        Rotate();
        Zoom();
        Follow();
    }

    private void Rotate()
    {
        yaw += _cameraInput.x * sensitivity.x;
        Quaternion currentRotation = Quaternion.Euler(45f, yaw * Mathf.Rad2Deg, 0);
        transform.position = target.position + currentRotation * originalOffset;
    }

    private void Zoom()
    {
        //Implement zoom functionality if needed
    }

    private void Follow()
    {
        transform.LookAt(target);
    }

    private void CameraRotate_performed(InputAction.CallbackContext context)
    {
        _cameraInput = context.ReadValue<Vector2>();
    }

    private void CameraRotate_canceled(InputAction.CallbackContext context)
    {
        _cameraInput = Vector2.zero;
    }
}


//    private void Update()
//    {
//        if (actions.Player.RightClick.ReadValue<float>() > 0.5f)
//        {
//            yaw += _cameraInput.x * sensitivity.x;
//            float targetPitch = pitch - _cameraInput.y * sensitivity.y;
//            // Clamp the pitch to prevent the camera from flipping over the top of the player
//            if (targetPitch * Mathf.Rad2Deg > -verticalPitch && targetPitch * Mathf.Rad2Deg < verticalPitch)
//            {
//                pitch = targetPitch;
//            }

//            Vector3 direction = new Vector3(Mathf.Sin(pitch) * Mathf.Sin(yaw),
//                                            Mathf.Cos(pitch),
//                                            Mathf.Sin(pitch) * Mathf.Cos(yaw));

//            transform.position = target.position + direction * distance;
//            transform.LookAt(target);
//        }
//        else
//        {
//            originalOffset = new Vector3(Mathf.Sin(pitch) * Mathf.Sin(yaw),
//                                        Mathf.Cos(pitch),
//                                        Mathf.Sin(pitch) * Mathf.Cos(yaw)) * distance;

//            transform.position = target.position + originalOffset;
//            transform.LookAt(target);
//        }
//    }

