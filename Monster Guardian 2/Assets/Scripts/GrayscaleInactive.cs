using UnityEngine;

public class GrayscaleInactive : MonoBehaviour
{
    public Material inactiveMaterial;
    private Material defaultMaterial;
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        defaultMaterial = renderer.material;
    }

    void OnDisable()
    {
        renderer.material = inactiveMaterial;
    }

    void OnEnable()
    {
        renderer.material = defaultMaterial;
    }
}
