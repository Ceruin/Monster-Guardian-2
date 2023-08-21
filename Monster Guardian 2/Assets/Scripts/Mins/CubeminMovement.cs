using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeminMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float followSpeed = 2f;
    private bool isFollowingPlayer = false;
    private Rigidbody rb;

    public Transform target;
    public float speed = 5f;
    public float stoppingDistance = 1f;
    private Vector3 desiredVelocity;

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

    private void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        desiredVelocity = direction * speed;

        // Calculate the force needed to reach the desired velocity
        Vector3 force = desiredVelocity - rb.velocity;
        force.y = 0; // Assuming you want to keep the AI character grounded

        // Apply the force to the Rigidbody
        rb.AddForce(force, ForceMode.Acceleration);

        //// Stop the AI character when it's close to the target
        //if (Vector3.Distance(transform.position, target.position) & lt; stoppingDistance)
        //{
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0);
        //}
    }

    public void SetFollowPlayer(bool follow)
    {
        isFollowingPlayer = follow;
    }
}
