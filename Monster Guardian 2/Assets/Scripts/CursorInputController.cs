using System.Collections;
using UnityEngine;

public class CursorInputController : MonoBehaviour
{
    public GameObject player; // the player character
    public GameObject gameCursor; // the game cursor
    public GameObject throwObjectPrefab; // the object to throw
    public LayerMask groundLayer; // ground layer mask
    public float throwStrengthMultiplier = 1f; // the throw strength multiplier
    public float throwSpeed = 10f; // the speed of the throw
    private PlayerControls actions;

    private Vector2 mousePos;

    private void Awake()
    {
        actions = new PlayerControls();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // get the mouse cursor position from Unity's new input system
        mousePos = actions.Player.Cursor.ReadValue<Vector2>();

        // check if the left mouse button was clicked
        if (actions.Player.Throw.triggered)
        {
            // calculate the throw direction and strength based on the cursor position and player position
            Vector3 throwDirection = (gameCursor.transform.position - player.transform.position).normalized;
            float throwStrength = Vector3.Distance(gameCursor.transform.position, player.transform.position) * throwStrengthMultiplier;

            // Call the throw function
            ThrowObject(gameCursor.transform.position, throwStrength);
        }
    }

    public float throwArcHeight = 2f; // The height of the throw arc above the player

    void ThrowObject(Vector3 targetPosition, float speed)
    {
        // Create a new instance of the object at the player's position
        GameObject thrownObject = Instantiate(throwObjectPrefab, player.transform.position, Quaternion.identity);

        // Calculate the direction to the target position
        Vector3 throwDirection = (targetPosition - player.transform.position).normalized;

        // Adjust the throw direction for the arc height
        throwDirection += Vector3.up * throwArcHeight;

        // Normalise the throw direction and multiply by the speed to get the velocity
        Vector3 throwVelocity = throwDirection.normalized * speed;

        // Add this velocity to the object
        Rigidbody rb = thrownObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = throwVelocity;
        }

        // Disable the collider on the thrown object for a short time
        Collider objectCollider = thrownObject.GetComponent<Collider>();
        if (objectCollider != null)
        {
            objectCollider.enabled = false;
            StartCoroutine(EnableColliderAfterDelay(objectCollider, 0.5f));
        }
    }



    IEnumerator EnableColliderAfterDelay(Collider collider, float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        // convert this to a Vector3, z value is not important here
        Vector3 mousePos3D = new Vector3(mousePos.x, mousePos.y, 0f);

        // generate a Ray from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(mousePos3D);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            // move the game cursor to hit point
            // add a small offset to the y coordinate to keep it slightly above the ground
            gameCursor.transform.position = hit.point + new Vector3(0, 0.01f, 0);

            // orient the game cursor to match the surface normal
            gameCursor.transform.up = hit.normal;
        }
    }
}
