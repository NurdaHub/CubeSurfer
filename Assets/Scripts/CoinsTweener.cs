using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinsTweener : MonoBehaviour
{
    [SerializeField] private Transform[] coins;
    private List<Vector3> _defaultPositions = new List<Vector3>();
    private Tweener _scaleTween;
    private float _endValue = 1;
    private float _scaleDuration = 0.4f;
    private float _moveDuration = 1;

    private void Start()
    {
        foreach (var coin in coins)
            _defaultPositions.Add(coin.GetComponent<RectTransform>().localPosition);
    }

    public void ResetCoins()
    {
        transform.localScale = Vector3.zero;
        
        for (int i = 0; i < coins.Length; i++)
            coins[i].GetComponent<RectTransform>().localPosition = _defaultPositions[i];
    }

    public void Scale(Vector3 targetPosition)
    {
        _scaleTween?.Kill();
        _scaleTween = transform.DOScale(_endValue, _scaleDuration).OnComplete(() => { MoveAllCoins(targetPosition); });
    }

    private void MoveAllCoins(Vector3 targetPosition)
    {
        foreach (var coin in coins)
            coin.DOMove(targetPosition, _moveDuration);
    }
}
