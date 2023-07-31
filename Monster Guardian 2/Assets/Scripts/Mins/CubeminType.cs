using UnityEngine;

public class CubeminType : MonoBehaviour
{
    public enum Type
    {
        Default,
    }

    public Type cubeminType;

    void Start()
    {
        switch (cubeminType)
        {
            case Type.Default:
                break;
        }
    }
}
