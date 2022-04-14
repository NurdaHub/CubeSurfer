using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private CubeManager _cubeManager;
    private ColorManager _colorManager;
    private string _lavaTag = "Lava";
    private bool _canTrigger = true;

    private void OnEnable()
    {
        _colorManager = GetComponent<ColorManager>();
        _cubeManager = GetComponent<CubeManager>();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (!_canTrigger)
            return;
        
        if (otherCollider.CompareTag(_lavaTag))
        {
            _cubeManager.OnWrongCube();
            return;
        }
        
        var colorManager = otherCollider.GetComponent<ColorManager>();
        if (colorManager == null)
            return;
        
        if (IsCorrectColor(colorManager))
            _cubeManager.OnCorrectCube(otherCollider.gameObject);
        else
        {
            _canTrigger = false;
            _cubeManager.OnWrongCube();
        }
    }

    private bool IsCorrectColor(ColorManager colorManager)
    {
        return colorManager.Color == _colorManager.Color;
    }

    public void SetTriggerState(bool value)
    {
        _canTrigger = value;
    }
}