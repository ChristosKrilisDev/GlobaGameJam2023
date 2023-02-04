using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public int groundTilesLeft, rootTilesLeft, level = 0;

    public static GameController Instance { get; private set; }

    public MapController mapController { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        mapController = GetComponentInChildren<MapController>();
        DontDestroyOnLoad(gameObject);
    }

    public void GoToNextLevel()
    {
        if(level > 0) mapController.CleanMap();
        level++;
        mapController.CreateMap();
    }

    // Start is called before the first frame update
    void Start()
    {
        GoToNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
