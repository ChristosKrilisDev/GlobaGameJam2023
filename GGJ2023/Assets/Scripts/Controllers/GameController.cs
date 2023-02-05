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

    [Space,Header("DATA")]
    public Save Save;
    public AssetsData AssetsData;
    [Space,Header("MUSIC")]
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _sfx;

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
        MusicSettings.Init(Save, _music, _sfx);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GoToNextLevel();
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
    }

}
