using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("PlayerShapes")]
    [SerializeField] private GameObject[] playerShapes;

    private Rigidbody rb;
    private Vector3[] originalScales;
    private int currentShapeIndex = 0;
    private bool isShifting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        originalScales = new Vector3[playerShapes.Length];
        for (int i = 0; i < playerShapes.Length; i++)
        {
            originalScales[i] = playerShapes[i].transform.localScale;
            playerShapes[i].SetActive(i == currentShapeIndex);

        }
    }

    void OnEnable()
    {
        GameManager.OnShapeChangeRequested += ChangeShape;
    }

    void OnDisable()
    {
        GameManager.OnShapeChangeRequested -= ChangeShape;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.isGameStarted == false) return;
        Move();
    }

    void Move()
    {
        rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime); ;
    }

    void ChangeShape(int shapeIndex)
    {
        if(GameManager.isGameStarted == false) return;

        if (isShifting || shapeIndex == currentShapeIndex || shapeIndex < 0 || shapeIndex > playerShapes.Length)
        {
            return;
        }

        StartCoroutine(SwitchShapeSmoothly(shapeIndex));
    }

    IEnumerator SwitchShapeSmoothly(int shapeIndex)
    {

        if (isShifting || shapeIndex == currentShapeIndex)
            yield break;

        isShifting = true;

        GameObject currentShape = playerShapes[currentShapeIndex];
        GameObject targetShape = playerShapes[shapeIndex];

        Vector3 fromScale = originalScales[currentShapeIndex];
        Vector3 toScale = originalScales[shapeIndex];


        for (float t = 0f; t < 1f; t += Time.deltaTime * 5f)
        {
            currentShape.transform.localScale = Vector3.Lerp(fromScale, Vector3.zero, t);
            yield return null;
        }

        currentShape.transform.localScale = Vector3.zero;
        currentShape.SetActive(false);
        targetShape.transform.localScale = Vector3.zero;
        targetShape.SetActive(true);

        for (float t = 0; t < 1; t += Time.deltaTime * 5f)
        {
            targetShape.transform.localScale = Vector3.Lerp(Vector3.zero, toScale, t);
            yield return null;
        }

        targetShape.transform.localScale = toScale;
        currentShapeIndex = shapeIndex;
        isShifting = false;
    }

    void OnTriggerEnter(Collider other)
    {
        string gateTag = other.tag;
        // Debug.Log("Player entered " + gateTag);
        string gateShape = gateTag.Replace("Gate", "");
        // Debug.Log("Gate shape: " + gateShape);
        string currentShapeTag = playerShapes[currentShapeIndex].tag;
        // Debug.Log("Player shape: " + currentShapeTag);
        if (gateShape != currentShapeTag)
        {
            Debug.Log("Wrong shape! Falling...");
            rb.useGravity = true; // turn gravity ON

            rb.AddForce(Vector3.down * 5f, ForceMode.VelocityChange);
            GameManager.isGameStarted = false;
        }
        else
        {
            Debug.Log("Correct shape: " + currentShapeTag);
            GameManager.instance.AddScore(10); 
            Destroy(other.gameObject, 2f);
            // Do nothing, allow passing
        }

    }



}
