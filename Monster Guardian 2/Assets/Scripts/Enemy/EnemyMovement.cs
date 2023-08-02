using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float crawlSpeed = 1f;
    public float jumpHeight = 5f; // You may need to adjust this value
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveToNextWaypoint();
    }

    private void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypoint];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * walkSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    public void Jump()
    {
        // Adding upward force to simulate a jump
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}
