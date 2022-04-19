using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private CoinsTweener _coinsTweener;
    [SerializeField] private Button _nextButton;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private GameObject _restartPanel;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private GameObject _eventDetector;
    [SerializeField] private Transform _coinTransform;

    private int _coins;
    private int _bonusCoins = 50;
    private float _waitTime = 1.5f;

    public void OnFinish()
    {
        SetEventDetectorState(false);
        _finishPanel.SetActive(true);
    }
    
    public void OnNextButtonClicked()
    {
        _coinsTweener.Scale(_coinTransform.position);
        _nextButton.interactable = false;
        StartCoroutine(CalculateCoins());
    }

    public void OnRestartButtonClicked()
    {
        _restartPanel.SetActive(false);
        _game.Restart();
    }

    public void OnLose()
    {
        SetEventDetectorState(false);
        _restartPanel.SetActive(true);
    }

    public void SetCoins(int value)
    {
        _coins = value;
        SetText(_coins.ToString());
    }

    public int GetCoins()
    {
        return _coins;
    }

    public void SetLevel(string value)
    {
        _levelText.text = value;
    }

    public void SetEventDetectorState(bool value)
    {
        _eventDetector.SetActive(value);
    }

    private IEnumerator CalculateCoins()
    {
        yield return new WaitForSeconds(_waitTime);
        
        _coins += _bonusCoins;
        SetText(_coins.ToString());
        
        yield return new WaitForSeconds(_waitTime);
        
        ResetFinishPanel();
        _game.NextLevel();
    }
    
    private void SetText(string text)
    {
        _coinsText.text = text;
    }

    private void ResetFinishPanel()
    {
        _finishPanel.SetActive(false);
        _nextButton.interactable = true;
        _coinsTweener.ResetCoins();
    }
}
