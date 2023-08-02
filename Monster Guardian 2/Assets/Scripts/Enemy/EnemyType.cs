using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public enum Type
    {
        Walking,
        Jumping,
        Flying,
    }

    public Type enemyType;

    void Start()
    {
        switch (enemyType)
        {
            case Type.Walking:
                // Initialize walking enemy
                GetComponent<EnemyMovement>().walkSpeed = 3f; // Example walking speed
                break;
            case Type.Jumping:
                // Initialize jumping enemy
                GetComponent<EnemyMovement>().jumpHeight = 2f; // Jump height
                GetComponent<EnemyMovement>().walkSpeed = 2f; // Walking speed with jumping ability
                break;
            case Type.Flying:
                // Initialize flying enemy
                //GetComponent<EnemyMovement>().isFlying = true; // Enable flying behavior
                break;
        }
    }
}
