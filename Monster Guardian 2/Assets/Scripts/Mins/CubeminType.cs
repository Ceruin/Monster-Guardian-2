using UnityEngine;

public class CubeminType : MonoBehaviour
{
    public enum Type
    {
        Damage,
        Shield,
        Distance,
    }

    public Type cubeminType;

    void Start()
    {
        switch (cubeminType)
        {
            case Type.Damage:
                // Initialize Cubemin with increased damage
                GetComponent<CubeminStats>().damage = 10; // Example damage value
                break;
            case Type.Shield:
                // Initialize Cubemin with increased shield
                GetComponent<CubeminStats>().shield = 5; // Example shield value
                break;
            case Type.Distance:
                // Initialize Cubemin with increased throw distance
                GetComponent<CubeminStats>().throwDistance = 5f; // Example throw distance
                break;
        }
    }
}
