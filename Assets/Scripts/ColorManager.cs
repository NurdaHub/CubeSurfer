using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    
    public Color Color { get; private set; }

    private void Start()
    {
        Color = _renderer.material.color;
    }
}
