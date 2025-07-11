using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("PlayerShapeChangeButton")]
    [SerializeField] private Button squareShapeButton;
    [SerializeField] private Button circleShapeButton;
    [SerializeField] private Button rectangleShapeButton;

    [Header("TextMeshPro")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreOnRestartText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI highScoreOnStartText;
    [SerializeField] private TextMeshProUGUI highScoreOnReStartText;

    [Header("Playback Controls")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button restartButton;

    [Header("Playback Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject restartPanel;

    [Header("Canvas")]
    [SerializeField] private Canvas inGameCanvas;
    [SerializeField] private Canvas playbackCanvas;

    void OnEnable()
    {
        GameManager.OnScoreChanged += UpdateScore;
        GameManager.OnHighScoreChanged += UpdateHighScore;
        GameManager.OnGameStarted += OnStartButtonClicked;
        GameManager.OnGamePaused += OnPauseButtonClicked;
        GameManager.OnGamePlayed += OnPlayButtonClicked;
        GameManager.OnPlayerDied += OnPlayerDied;
    }

    void Start()
    {
        ButtonClickHandle();
        SetInitialUIState();
    }

    void ButtonClickHandle()
    {
        squareShapeButton.onClick.AddListener(() => GameManager.OnShapeChangeRequested?.Invoke(0));
        circleShapeButton.onClick.AddListener(() => GameManager.OnShapeChangeRequested?.Invoke(1));
        rectangleShapeButton.onClick.AddListener(() => GameManager.OnShapeChangeRequested?.Invoke(2));

        //PlayBack Controls
        startButton.onClick.AddListener(() => GameManager.instance.OnGameStart());
        pauseButton.onClick.AddListener(() => GameManager.instance.OnGamePause());
        playButton.onClick.AddListener(() => GameManager.instance.OnGamePlay());
        restartButton.onClick.AddListener(() => { GameManager.instance.OnGameReset(); SetInitialUIState(); });
    }

    private void SetInitialUIState()
    {
        startPanel.SetActive(true);
        pausePanel.SetActive(false);
        playPanel.SetActive(false);
        restartPanel.SetActive(false);
        countdownText.gameObject.SetActive(false);
        playbackCanvas.gameObject.SetActive(true);
        inGameCanvas.gameObject.SetActive(false);
    }

    private IEnumerator CountdownToStart()
    {
        countdownText.gameObject.SetActive(true);
        int count = 5;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        countdownText.gameObject.SetActive(false);
        GameManager.instance.OnGamePlay();
    }

    private void OnStartButtonClicked()
    {
        startPanel.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(CountdownToStart());
        pausePanel.SetActive(true);
        inGameCanvas.gameObject.SetActive(true);

    }

    private void OnPauseButtonClicked()
    {
        playPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    private void OnPlayButtonClicked()
    {
        playPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    private void OnPlayerDied()
    {
        restartPanel.SetActive(true);
        pausePanel.SetActive(false);
        inGameCanvas.gameObject.SetActive(false);
    }

    private void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
        scoreOnRestartText.text = "Score: " + score;
    }

    private void UpdateHighScore(int highScore)
    {
        highScoreText.text = "High Score: " + highScore;
        highScoreOnStartText.text = "High Score: " + highScore;
        highScoreOnReStartText.text = "High Score: " + highScore;
    }
    
    void OnDisable()
    {
        GameManager.OnScoreChanged -= UpdateScore;
        GameManager.OnHighScoreChanged -= UpdateHighScore;
        GameManager.OnGameStarted -= OnStartButtonClicked;
        GameManager.OnGamePaused -= OnPauseButtonClicked;
        GameManager.OnGamePlayed -= OnPlayButtonClicked;
        GameManager.OnPlayerDied -= OnPlayerDied;
    }

}
