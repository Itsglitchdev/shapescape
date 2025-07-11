using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public static Action<int> OnShapeChangeRequested;
    public static Action<int> OnScoreChanged;

    public static bool isGameStarted = true;
    private int score = 0;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AddScore(0);
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        OnScoreChanged?.Invoke(score);
    }




}
