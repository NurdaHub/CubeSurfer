using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public UIManager UIManager;
    public CubesContainer CubesContainer;
    public CollisionDetector DefaultCubeDetector;
    private int _sceneId = 1;
    private int _loopLevelBegin = 2;
    private int _sceneCount;
    private const string _sceneIdKey = "SceneIdKey";
    private const string _coinsCountKey = "CoinsCountKey";

    private void Start()
    {
        Load();
        _sceneCount = SceneManager.sceneCountInBuildSettings;
        LoadLevel();
    }

    public void Win()
    {
        Debug.Log("win");
        PlayerMovement.StopMovement();
        UIManager.OnFinish();
    }

    public void Lose()
    {
        Debug.Log("lose");
        PlayerMovement.StopMovement();
        UIManager.OnLose();
    }

    public void NextLevel()
    {
        SceneManager.UnloadSceneAsync(_sceneId);
        _sceneId++;
        
        if (_sceneId >= _sceneCount)
            _sceneId = _loopLevelBegin;
        
        LoadLevel();
        Save();
    }

    public void LoadLevel()
    {
        CubesContainer.ClearAllCubes();
        SceneManager.LoadScene(_sceneId, LoadSceneMode.Additive);
        PlayerMovement.ResetPosition();
        UIManager.SetLevel(_sceneId.ToString());
    }

    public void Restart()
    {
        SceneManager.UnloadSceneAsync(_sceneId);
        LoadLevel();
        DefaultCubeDetector.SetTriggerState(true);
    }

    private void Save()
    {
        var coins = UIManager.GetCoins();
        PlayerPrefs.SetInt(_sceneIdKey, _sceneId);
        PlayerPrefs.SetInt(_coinsCountKey, coins);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(_sceneIdKey))
            _sceneId = PlayerPrefs.GetInt(_sceneIdKey);
        
        if (PlayerPrefs.HasKey(_coinsCountKey))
        {
            var coins = PlayerPrefs.GetInt(_coinsCountKey);
            UIManager.SetCoins(coins);
        }
    }
}
