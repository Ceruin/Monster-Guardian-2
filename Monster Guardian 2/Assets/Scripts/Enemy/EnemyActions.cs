using UnityEngine;

public class EnemyActions : MonoBehaviour, IDamageable
{
    private enum State
    {
        Idle,
        Wandering,
        Patrolling,
        Attacking,
        Eat,
        Slam
    }

    public int health = 50; // You can adjust the initial health value as needed
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;
    private int currentPatrolIndex = 0;
    private State currentState = State.Idle;
    private EnemyMovement movement;

    void Start()
    {
        movement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                // Handle idle behavior
                break;
            case State.Wandering:
                Wander();
                break;
            case State.Patrolling:
                Patrol();
                break;
        }
    }

    private void Wander()
    {
        // Wandering behavior can be implemented using a random target selection and moving towards that target
    }

    private void Patrol()
    {
        if (movement && patrolPoints.Length > 0)
        {
            Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
            Vector3 direction = (targetPatrolPoint.position - transform.position).normalized;
            movement.GetComponent<Rigidbody>().velocity = direction * moveSpeed;

            if (Vector3.Distance(transform.position, targetPatrolPoint.position) < 0.1f)
            {
                movement.GetComponent<Rigidbody>().velocity = Vector3.zero; // Stop the rigidbody from moving
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            }
        }
    }


    public void ShakeOffCubemins()
    {
        foreach (Transform child in transform)
        {
            CubeminActions cubemin = child.GetComponent<CubeminActions>();
            if (cubemin)
            {
                cubemin.StopAttacking();
            }
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            // Handle enemy death, e.g., play animation and remove from the game
            health = 0; // Ensure health doesn't go negative
            Debug.Log("Enemy has been defeated!");
            Destroy(gameObject); // Removes the enemy from the game
                                 // You may want to add additional code to handle rewards, drop items, etc.
        }
        else
        {
            Debug.Log("Enemy took damage! Remaining health: " + health);
            // You may want to add code here to trigger a damage animation, play a sound, etc.
        }
    }
}
