using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private BoxCollider collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        // Update the size of the collider to match the sprite bounds
        collider.size = spriteRenderer.bounds.size;

        // Update the position of the collider to match the sprite bounds
        collider.center = spriteRenderer.bounds.center;
    }
}
