using System.Collections.Generic;
using UnityEngine;

public class HollowShapeManager : MonoBehaviour
{
    [Header("HollowShapes")]
    [SerializeField] private GameObject[] HollowShapePrefabs;

    [Header("Player")]
    [SerializeField] private Transform playerTransform;

    [Header("Road Reference")]
    [SerializeField] private RoadManager roadManager;

    private float holeShapePositionZ = 30f;
    private float gapBetweenTwoShapesHoles = 20f;
    private List<GameObject> HollowShape = new List<GameObject>();

    private bool initialSpawnDone = false;

    void Start()
    {
        InitializeHollowShape(5);
        initialSpawnDone = true;
    }


    void Update()
    {
        if(GameManager.isGameStarted == false) return;

        if (initialSpawnDone && holeShapePositionZ < roadManager.GetRoadEndZ())
        {
            int randomCount = Random.Range(3, 8);
            InitializeHollowShape(randomCount);
        }
    }


    void InitializeHollowShape(int count)
    {

        for (int i = 0; i < count; i++)
        {
            if (holeShapePositionZ > roadManager.GetRoadEndZ())
            {
                return;
            }
            
            int randomHoleShapeIndex = Random.Range(0, HollowShapePrefabs.Length);
            GameObject prefab = HollowShapePrefabs[randomHoleShapeIndex];
            Vector3 postion = new Vector3(0, 0, holeShapePositionZ);
            GameObject holeObject = Instantiate(prefab, postion, prefab.transform.rotation ,transform);
            HollowShape.Add(holeObject);
            holeShapePositionZ += gapBetweenTwoShapesHoles;

        }

    }




}