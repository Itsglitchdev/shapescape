using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("PlayerShapeChangeButton")]
    [SerializeField] private Button squareShapeButton;
    [SerializeField] private Button circleShapeButton;
    [SerializeField] private Button rectangleShapeButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
}
