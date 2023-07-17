using UnityEngine;

public class SpinItem : MonoBehaviour
{
    public float rotationSpeed = 50f; // Adjust to your preference

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
    }
}
