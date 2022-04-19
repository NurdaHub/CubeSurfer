using UnityEngine;

public class FinishDetector : MonoBehaviour
{
    [SerializeField] private Game _game;
    private string finishTag = "Finish";
    
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag(finishTag))
            _game.Win();
    }
}
