using UnityEngine;

public class SpinItem : MonoBehaviour
{
    public float spinSpeed = 50.0f; // Speed of the spin

    void Update()
    {
        // Rotate around Z axis
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}
