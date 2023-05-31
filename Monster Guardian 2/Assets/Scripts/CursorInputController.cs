using UnityEngine;

public class CursorInputController : MonoBehaviour
{
    public GameObject player; // the player character
    public GameObject gameCursor; // the game cursor
    public GameObject throwParticlePrefab; // the particle system prefab
    public LayerMask groundLayer; // ground layer mask
    public float throwStrengthMultiplier = 1f; // the throw strength multiplier
    private PlayerControls actions;

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
        Vector2 mousePos = actions.Player.Cursor.ReadValue<Vector2>();

        // convert this to a Vector3, z value is not important here
        Vector3 mousePos3D = new Vector3(mousePos.x, mousePos.y, 0f);

        // generate a Ray from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(mousePos3D);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            // move the game cursor to hit point
            gameCursor.transform.position = hit.point;

            // calculate the throw direction and strength based on the cursor position and player position
            Vector3 throwDirection = (gameCursor.transform.position - player.transform.position).normalized;
            float throwStrength = Vector3.Distance(gameCursor.transform.position, player.transform.position) * throwStrengthMultiplier;

            // check if the left mouse button was clicked
            if (actions.Player.Throw.ReadValue<float>() > 0)
            {
                // spawn the particle system at the throw location
                GameObject throwParticle = Instantiate(throwParticlePrefab, gameCursor.transform.position, Quaternion.identity);

                // destroy the particle system after its duration
                Destroy(throwParticle, throwParticle.GetComponent<ParticleSystem>().main.duration);

                // Here you should call the function that performs the throw action, using throwDirection and throwStrength
                // throwPikmin(throwDirection, throwStrength);
            }
        }
    }
}
