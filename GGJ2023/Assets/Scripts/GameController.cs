using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{

    public int groundTilesLeft, rootTilesLeft, level = 0;

    public static GameController Instance { get; private set; }

    public MapController mapController { get; private set; }
    public GuiManager GuiController { get; private set; }
    public ScoreManager ScoreManager { get; set; }


    public UnityAction<int> OnLevelChange;
    public UnityAction<int> OnScoreChange;

    private void Awake()
    {

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
        OnLevelChange?.Invoke(level);
        mapController.CreateMap();
    }

    void Start()
    {
        GoToNextLevel();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            ScoreManager.IncreaseScore();
        
    }

}
