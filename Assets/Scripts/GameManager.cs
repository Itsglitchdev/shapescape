using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public static Action<int> OnShapeChangeRequested;

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


}
