using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ColliderController : MonoBehaviour
{
    private BoxCollider _collider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Create a cube collider around the sprite
        Vector2 spriteSize = _spriteRenderer.sprite.bounds.size;
        Vector3 parentScale = _spriteRenderer.transform.lossyScale;

        float maxDimension = Mathf.Max(spriteSize.x * parentScale.x, spriteSize.y * parentScale.y);
        _collider.size = new Vector3(maxDimension, maxDimension, maxDimension);

        // Center the collider
        _collider.center = _spriteRenderer.bounds.center - transform.position;
    }
}
