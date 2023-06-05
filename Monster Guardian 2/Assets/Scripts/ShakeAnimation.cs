using UnityEngine;

public class ShakeAnimation : MonoBehaviour
{
    public float amplitude = 0.1f; // Amplitude of the shake
    public float frequency = 2.0f; // Frequency of the shake

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Vertical shake using a sinewave over time
        float yOffset = amplitude * Mathf.Sin(Time.time * frequency);
        transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);
    }
}
