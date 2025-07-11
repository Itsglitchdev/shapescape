using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string HightScoreKey = "HighScore";

    public static GameManager instance;
    public static Action<int> OnShapeChangeRequested;
    public static Action<int> OnScoreChanged;
    public static Action<int> OnHighScoreChanged;
    public static Action<bool> OnPlayerVisibilityChanged;
    public static Action<bool> OnHollowShapeVisibilityChanged;
    public static Action<bool> OnRoadVisibilityChanged;

    public static Action OnGameStarted;
    public static Action OnGamePaused;
    public static Action OnGamePlayed;
    public static Action OnPlayerDied;

    public static bool isGameStarted = false;
    private int score;
    private int highScore;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt(HightScoreKey, 0);
        GetBestScore(highScore);
        AddScore(0);
        isGameStarted = false;
        OnScoreChanged?.Invoke(score);
        OnPlayerVisibilityChanged?.Invoke(false);
        OnHollowShapeVisibilityChanged?.Invoke(false);
        OnRoadVisibilityChanged?.Invoke(false);
        // OnGameStarted?.Invoke(); 
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        OnScoreChanged?.Invoke(score);
    }

    public void OnGameStart()
    {
        OnPlayerVisibilityChanged?.Invoke(true);
        OnHollowShapeVisibilityChanged?.Invoke(true);
        OnRoadVisibilityChanged?.Invoke(true);
        OnGameStarted?.Invoke();
    }

    public void OnGamePause()
    {
        isGameStarted = false;
        OnGamePaused?.Invoke();
    }

    public void OnGamePlay()
    {
        isGameStarted = true;
        OnGamePlayed?.Invoke();
    }

    public void OnGameOver()
    {
        isGameStarted = false;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HightScoreKey, highScore);
            PlayerPrefs.Save();
        }
        GetBestScore(highScore);
        OnPlayerDied?.Invoke();
    }

    public void OnGameReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GetBestScore(int bestScore)
    {
        OnHighScoreChanged?.Invoke(bestScore);
    }

    
    

}
