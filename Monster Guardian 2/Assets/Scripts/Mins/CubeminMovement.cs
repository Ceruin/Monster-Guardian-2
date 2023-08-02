using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeminMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float followSpeed = 2f;
    private bool isFollowingPlayer = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isFollowingPlayer)
        {
            FollowPlayer();
        }
    }

    public void FollowPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * followSpeed * Time.fixedDeltaTime);
    }

    public void SetFollowPlayer(bool follow)
    {
        isFollowingPlayer = follow;
    }
}
