using DG.Tweening;
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
    

    private void OnEnable()
    {
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

    private void OnMusicChange(bool isMuted)
    {
        _musicButton.image.sprite = isMuted? _musicOff : _musicOn;
    }
    
    private void AddDelegates()
    {
        GameController.Instance.OnLevelChange += OnLevelChange;
        GameController.Instance.OnScoreChange += OnScoreChange;
        GameController.Instance.OnMusicChange += OnMusicChange;
        
        _musicButton.onClick.AddListener(MusicSettings.OnMusicChange);
    }

    private void RemoveDelegates()
    {
        GameController.Instance.OnLevelChange -= OnLevelChange;
        GameController.Instance.OnScoreChange -= OnScoreChange;
        GameController.Instance.OnMusicChange -= OnMusicChange;
        
        _musicButton.onClick.RemoveListener(MusicSettings.OnMusicChange);

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
