using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        // Update the size of the collider to match the sprite bounds
        collider.size = spriteRenderer.bounds.size;

        // Update the position of the collider to match the sprite bounds
        // Convert from world space to local space
        collider.center = transform.InverseTransformPoint(spriteRenderer.bounds.center);
    }
}
