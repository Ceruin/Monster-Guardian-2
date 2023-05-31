using UnityEngine;

public class CursorInputController : MonoBehaviour
{
    public GameObject player; // the player character
    public GameObject gameCursor; // the game cursor
    public GameObject throwParticlePrefab; // the particle system prefab
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
        if (actions.Player.Throw.ReadValue<float>() > 0)
        {
            // calculate the throw direction and strength based on the cursor position and player position
            Vector3 throwDirection = (gameCursor.transform.position - player.transform.position).normalized;
            float throwStrength = Vector3.Distance(gameCursor.transform.position, player.transform.position) * throwStrengthMultiplier;

            // spawn the particle system at the throw location
            GameObject throwParticle = Instantiate(throwParticlePrefab, gameCursor.transform.position, Quaternion.identity);

            // destroy the particle system after its duration
            Destroy(throwParticle, throwParticle.GetComponent<ParticleSystem>().main.duration);

            // Call the throw function
            ThrowObject(gameCursor.transform.position);
        }
    }

    void ThrowObject(Vector3 targetPosition)
    {
        // create a new instance of the object at the player's position
        GameObject thrownObject = Instantiate(throwObjectPrefab, player.transform.position, Quaternion.identity);

        // calculate the initial velocity required to reach the target position
        Vector3 throwDirection = (targetPosition - player.transform.position).normalized;
        Vector3 throwVelocity = throwDirection * throwSpeed;

        // add this velocity to the object
        Rigidbody rb = thrownObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = throwVelocity;
        }
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
