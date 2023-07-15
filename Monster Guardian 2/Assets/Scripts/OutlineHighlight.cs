using UnityEngine;

public class OutlineHighlight : MonoBehaviour
{
    public Material highlightMaterial;
    private Material defaultMaterial;
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        defaultMaterial = renderer.material;
    }

    void OnMouseEnter()
    {
        renderer.material = highlightMaterial;
    }

    void OnMouseExit()
    {
        renderer.material = defaultMaterial;
    }
}
