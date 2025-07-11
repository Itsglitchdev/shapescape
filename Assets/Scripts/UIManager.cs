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

    void OnEnable()
    {
        GameManager.OnScoreChanged += UpdateScore;
    }

    void OnDisable()
    {
        GameManager.OnScoreChanged -= UpdateScore;
    }

    void Start()
    {
        ButtonClickHandle();
    }

    void ButtonClickHandle()
    {
        squareShapeButton.onClick.AddListener(() => GameManager.OnShapeChangeRequested?.Invoke(0));
        circleShapeButton.onClick.AddListener(() => GameManager.OnShapeChangeRequested?.Invoke(1));
        rectangleShapeButton.onClick.AddListener(() => GameManager.OnShapeChangeRequested?.Invoke(2));
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
