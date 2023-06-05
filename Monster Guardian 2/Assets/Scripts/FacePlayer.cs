using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Vector3 playerPosition;

    void Update()
    {
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
