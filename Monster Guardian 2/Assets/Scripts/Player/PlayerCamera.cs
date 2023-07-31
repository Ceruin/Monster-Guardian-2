using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public float distance = 10.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    void LateUpdate()
    {
        float wantedRotationAngle = player.eulerAngles.y;
        float wantedHeight = player.position.y + height;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        transform.position = player.position;
        transform.position -= currentRotation * Vector3.forward * distance;
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        transform.LookAt(player);
    }
}


//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerCamera : MonoBehaviour
//{
//    private Vector2 _cameraInput;
//    private float yaw = 0.0f;
//    private float pitch = 0.0f;
//    public Transform target;
//    public Vector2 sensitivity = new Vector2(0.01f, 0.01f);
//    public float distance = 10.0f;
//    private PlayerControls actions;
//    private Vector3 originalOffset;
//    private int verticalPitch = 85;

//    private void Awake()
//    {
//        actions = new PlayerControls();

//        actions.Player.RightClick.performed += RightClick_performed;
//        actions.Player.RightClick.canceled += RightClick_canceled;
//        actions.Player.CameraRotate.performed += CameraRotate_performed;
//        actions.Player.CameraRotate.canceled += CameraRotate_canceled;

//        actions.Enable();

//        Cursor.lockState = CursorLockMode.None;

//        originalOffset = transform.position - target.position;

//        distance = originalOffset.magnitude;
//        yaw = Mathf.Atan2(originalOffset.x, originalOffset.z);
//        pitch = Mathf.Asin(originalOffset.y / distance);
//    }

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

//    private void RightClick_canceled(InputAction.CallbackContext obj)
//    {
//        _cameraInput = Vector2.zero;
//        originalOffset = transform.position - target.position;
//    }

//    private void RightClick_performed(InputAction.CallbackContext obj)
//    {
//    }

//    private void CameraRotate_canceled(InputAction.CallbackContext context)
//    {
//        _cameraInput = Vector2.zero;
//    }

//    private void CameraRotate_performed(InputAction.CallbackContext context)
//    {
//        _cameraInput = context.ReadValue<Vector2>();
//    }
//}
