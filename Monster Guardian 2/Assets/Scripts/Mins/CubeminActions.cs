using System.Collections;
using UnityEngine;

public class CubeminActions : MonoBehaviour, IDamageable
{
    public int health = 30; // You can adjust the initial health value as needed
    public int attackPower = 1;
    public GameObject enemy;
    public Transform playerTransform;
    public float followDistance = 5f; // Distance at which Cubemin starts following the player
    private States currentState = States.Idle;

    private CubeminMovement movement; // Reference to the movement script

    enum States
    {
        Idle,
        Follow,
        Task,
        Thrown,
        Attacking,
        Grounded,
        Latched
    }

    void Start()
    {
        movement = GetComponent<CubeminMovement>();
    }

    void Update()
    {
        switch (currentState)
        {
            case States.Idle:
                if (Vector3.Distance(transform.position, playerTransform.position) < followDistance)
                {
                    currentState = States.Follow;
                    movement.SetFollowPlayer(true);
                }
                break;

            case States.Follow:
                if (Vector3.Distance(transform.position, playerTransform.position) >= followDistance)
                {
                    currentState = States.Idle;
                    movement.SetFollowPlayer(false);
                }
                break;

                // You can add logic for other states here
        }
    }

    public void AttackEnemy()
    {
        currentState = States.Attacking;
        transform.SetParent(enemy.transform);
        StartCoroutine(DealDamage());
    }

    private IEnumerator DealDamage()
    {
        while (true)
        {
            enemy.GetComponent<IDamageable>().TakeDamage(attackPower);
            yield return new WaitForSeconds(1);
        }
    }

    public void StopAttacking()
    {
        currentState = States.Idle;
        StopAllCoroutines();
        transform.SetParent(null);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Cubemin has been defeated!");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Cubemin took damage! Remaining health: " + health);
        }
    }
}
