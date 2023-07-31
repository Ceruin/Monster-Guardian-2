using UnityEngine;

public class CubeminMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float followSpeed = 2f;
    private bool isFollowingPlayer = false;

    void Update()
    {
        if (isFollowingPlayer)
        {
            FollowPlayer();
        }
    }

    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, followSpeed * Time.deltaTime);
    }

    public void SetFollowPlayer(bool follow)
    {
        isFollowingPlayer = follow;
    }
}
