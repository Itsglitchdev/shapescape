using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RoadManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject roadPrefab;

    [Header("Player")]
    [SerializeField] private Transform playerTransform;
    private float roadPositionZ = 80f;
    private float roadPositionY = -1.5f;
    private float roadSafeZone = 300f;
    private float roadDeadZone = 150f;
    private List<GameObject> activePieces = new List<GameObject>();


    void Start()
    {
        InitializeRoads();
    }

    void OnEnable()
    {
        GameManager.OnPlayerVisibilityChanged += HandleVisibility;
    }

    void OnDisable()
    {
        GameManager.OnPlayerVisibilityChanged -= HandleVisibility;
    }

    void Update()
    {
        if (GameManager.isGameStarted == false) return;

        GetRoadEndZ();

        if (playerTransform.position.z + roadSafeZone > roadPositionZ)
        {
            InitializeRoads();
        }

        DestroyRoads();
    }

    void InitializeRoads()
    {
        GameObject road = Instantiate(roadPrefab, new Vector3(0, roadPositionY, roadPositionZ), Quaternion.identity, transform);
        Renderer roadRenderer = road.GetComponent<Renderer>();
        if (roadRenderer != null)
        {
            float sizeRoad = roadRenderer.bounds.size.z;
            roadPositionZ += sizeRoad;
            activePieces.Add(road);
        }
    }

    void DestroyRoads()
    {
        if (activePieces.Count > 0)
        {
            GameObject firstPiece = activePieces[0];

            if (playerTransform.transform.position.z - roadDeadZone > firstPiece.transform.position.z)
            {
                activePieces.RemoveAt(0);
                Destroy(firstPiece);
            }
        }
    }

    void HandleVisibility(bool isVisible)
    {
        foreach (GameObject road in activePieces)
        {
            road.SetActive(isVisible);
        }
    }

    public float GetRoadEndZ()
    {
        // Debug.Log(roadPositionZ);
        return roadPositionZ;
    }


}
