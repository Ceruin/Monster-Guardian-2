using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float crawlSpeed = 1f;
    public float jumpHeight = 1f;
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveToNextWaypoint();
    }

    private void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypoint];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        controller.Move(direction * walkSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    public void Jump()
    {
        controller.Move(Vector3.up * jumpHeight);
    }
}
