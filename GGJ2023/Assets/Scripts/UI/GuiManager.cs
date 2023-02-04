using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GuiManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _scoreTxt;
    [SerializeField] private TextMeshProUGUI _levelTxt;

    public UnityAction<int> OnScoreChanged;

    private void ChangeScore()
    {
        var x = OnScoreChanged;
        _scoreTxt.text = x.ToString();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        _scoreTxt.text = "0" ;
        _levelTxt.text = GameController.Instance.level.ToString();
        
        AddDelegates();
    }

    private void OnScoreChange()
    {
        //change
        _levelTxt.text = GameController.Instance.level.ToString();
        PopUpAnimation(_levelTxt.transform);
    }
    
    private void OnLevelChange(int level)
    {
        OnScoreChange();
        _levelTxt.text = GameController.Instance.level.ToString();
        PopUpAnimation(_levelTxt.transform);
    }


    private void AddDelegates()
    {
        GameController.Instance.OnLevelChange += OnLevelChange;

    }

    private void RemoveDelegates()
    {
        GameController.Instance.OnLevelChange -= OnLevelChange;

    }
    
    
    
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
    
}
