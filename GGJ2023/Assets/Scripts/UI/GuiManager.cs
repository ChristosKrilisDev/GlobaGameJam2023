using DG.Tweening;
using GUI;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    [Space, Header("Text")]
    [SerializeField] private TextMeshProUGUI _scoreTxt;
    [SerializeField] private TextMeshProUGUI _levelTxt;

    [Space, Header("Sound")]
    [SerializeField] private Button _musicButton;
    [SerializeField] private Sprite _musicOn;
    [SerializeField] private Sprite _musicOff;

    [SerializeField] private StatSlotGui _rootSlot, _gemSlots, _groundSlots, _radarSlots;
    

    private void OnEnable()
    {
        GameController.Instance.GuiController = this;
        Init();
    }

    private void Init()
    {
        _scoreTxt.text = "0" ;
        _levelTxt.text = GameController.Instance.Level.ToString();
        
        AddDelegates();
    }

    private void OnScoreChange(int value)
    {
        _scoreTxt.text = value.ToString();
        PopUpAnimation(_scoreTxt.transform);
        //todo: play sfx
    }
    
    private void OnLevelChange(int value)
    {
        _levelTxt.text = value.ToString();
        PopUpAnimation(_levelTxt.transform);
        //todo: play sfx

    }

    private void OnRootChanged(int value)
    {
        _rootSlot.ChangeText($"{value}");
    }
    
    private void OnGemChanged(int value)
    {
        _gemSlots.ChangeText($"{value}");
    }
    
    private void OnGroundChanged(int value)
    {
        _groundSlots.ChangeText($"{value}");
    }
    
    private void OnRadarChanged(int value)
    {
        _radarSlots.ChangeText($"{value}");
    }

    private void OnMusicChange(bool isMuted)
    {
        _musicButton.image.sprite = isMuted? _musicOff : _musicOn;
    }
    
    private void AddDelegates()
    {
        GameController.Instance.OnLevelChange += OnLevelChange;
        GameController.Instance.OnScoreChange += OnScoreChange;
        GameController.Instance.OnMusicChange += OnMusicChange;
        
        GameController.Instance.OnRootChanged += OnRootChanged;
        GameController.Instance.OnGemChanged += OnGemChanged;
        GameController.Instance.OnGroundChanged += OnGroundChanged;
        GameController.Instance.OnRadarChanged += OnRadarChanged;
        
        _musicButton.onClick.AddListener(MusicSettings.OnMusicChange);
    }

    private void RemoveDelegates()
    {
        GameController.Instance.OnLevelChange -= OnLevelChange;
        GameController.Instance.OnScoreChange -= OnScoreChange;
        GameController.Instance.OnMusicChange -= OnMusicChange;
        
        GameController.Instance.OnRootChanged -= OnRootChanged;
        GameController.Instance.OnGemChanged -= OnGemChanged;
        GameController.Instance.OnGroundChanged -= OnGroundChanged;
        GameController.Instance.OnRadarChanged -= OnRadarChanged;
        
        _musicButton.onClick.RemoveListener(MusicSettings.OnMusicChange);

    }

    public void UpdateSlots()
    {
        _rootSlot.ChangeText($"{GameController.Instance.RootTilesLeft}");
        _gemSlots.ChangeText($"{GameController.Instance.GemTilesLeft}");
        _groundSlots.ChangeText($"{GameController.Instance.GroundTilesLeft}");
        _radarSlots.ChangeText($"{GameController.Instance.CharacterController.CharacterParams.RadarsSpawnLimit - GameController.Instance.CharacterController.CharacterParams.CurrentRadarsSpawned}");
    }

#region Animations

    private void PopUpAnimation(Transform objTrasform)
    {
        var scale = Vector3.one;
        
        objTrasform.transform.DOKill();
        objTrasform.transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.25f).SetEase(Ease.OutFlash).OnComplete(() =>
        {
            objTrasform.transform.DOScale(scale, 0.25f);
        });
        
        objTrasform.transform.localScale = scale;
    }

#endregion

    
}
