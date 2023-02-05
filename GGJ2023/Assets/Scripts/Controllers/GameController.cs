using Data;
using Enums;
using Settings;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{

    [HideInInspector]
    public int GroundTilesLeft, RootTilesLeft, GemTilesLeft, Level = 0;

    [HideInInspector]
    public GameState gameState = GameState.Normal;

    public MapController MapController { get; private set; }

    public GuiManager GuiController { get; set; }
    public ScoreManager ScoreManager { get; set; }
    [SerializeField] private CameraController _cameraController;
    
    [Space,Header("DATA")]
    public Save Save;
    public AssetsData AssetsData;
    [Space,Header("MUSIC")]
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _sfx;


    [HideInInspector]
    public CharacterControls CharacterController;
    
    public UnityAction<int> OnLevelChange;
    public UnityAction<int> OnScoreChange;
    public UnityAction<bool> OnMusicChange;
    
    public UnityAction<int> OnRootChanged;
    public UnityAction<int> OnGemChanged;
    public UnityAction<int> OnGroundChanged;
    public UnityAction<int> OnRadarChanged;
    
    public static GameController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;
        CharacterController = FindObjectOfType<CharacterControls>(true);
        MapController = GetComponentInChildren<MapController>();
        MusicSettings.Init(Save, _music, _sfx);
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GoToNextLevel();
    }

    public void IncreaseCounters(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Ground:
                OnGroundChanged?.Invoke(GroundTilesLeft);
                break;
            case TileType.Root:
                OnRootChanged?.Invoke(RootTilesLeft);
                break;
            case TileType.Gem:
                OnGroundChanged?.Invoke(GroundTilesLeft);
                OnGemChanged?.Invoke(GemTilesLeft);
                break;
        }
    }


    public void GoToNextLevel()
    {
        if (Level >= 4)
        {
            GameObject.Find("ScreenLoader").GetComponent<ScreenLoader>().LoadScene(0);
        }
        else
        {
            if (Level > 0) MapController.CleanMap();
            Level++;
            OnLevelChange?.Invoke(Level);
            _cameraController.MoveCamera();
            StartCoroutine(_cameraController.ChangeCameraSize());
            MapController.CreateMap();
        }

        GuiController.UpdateSlots();

        gameState = GameState.Normal;
    }
}

public enum GameState
{
    Normal,
    Transition
}
