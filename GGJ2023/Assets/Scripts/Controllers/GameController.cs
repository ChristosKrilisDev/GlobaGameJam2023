using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Settings;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{

    [HideInInspector]
    public int GroundTilesLeft, RootTilesLeft, Level = 0;

    public MapController MapController { get; private set; }

    public GuiManager GuiController { get; set; }
    public ScoreManager ScoreManager { get; set; }
    [SerializeField] private CameraController _cameraController;

    public UnityAction<int> OnLevelChange;
    public UnityAction<int> OnScoreChange;
    public UnityAction<bool> OnMusicChange;

    public Save Save;
    public MusicSettings MusicSettings;
    
    
    
    public static GameController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        MapController = GetComponentInChildren<MapController>();
        MusicSettings = new MusicSettings(Save);
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        GoToNextLevel();
    }

    public void GoToNextLevel()
    {
        if(Level > 0) MapController.CleanMap();
        Level++;
        OnLevelChange?.Invoke(Level);
        _cameraController.MoveCamera();
        StartCoroutine(_cameraController.ChangeCameraSize());
        MapController.CreateMap();
    }

    
    


}
