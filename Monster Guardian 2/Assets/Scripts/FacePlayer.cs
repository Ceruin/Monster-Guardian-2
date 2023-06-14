using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform playerTransform;

    private void Update()
    {
        Vector3 targetPosition = new Vector3(playerTransform.position.x,
                                             transform.position.y,
                                             playerTransform.position.z);
        transform.LookAt(targetPosition);
    }
}
