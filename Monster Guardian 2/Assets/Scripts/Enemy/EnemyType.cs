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
                break;
            case Type.Jumping:
                // Initialize jumping enemy
                GetComponent<EnemyMovement>().jumpHeight = 2f; // Example customization for a jumping enemy
                break;
            case Type.Flying:
                // Initialize flying enemy
                break;
        }
    }
}
