using UnityEngine;

public class EnemyActions : MonoBehaviour, IDamageable
{
    private enum State
    {
        Idle,
        Wandering,
        Patrolling,
    }

    public int health = 50; // You can adjust the initial health value as needed

    private State currentState = State.Idle;
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    public float moveSpeed = 2f;

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
        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPatrolPoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPatrolPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
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
