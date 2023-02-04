using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GuiManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _scoreTxt;

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
        _scoreTxt.text = "tetsa";
    }

}
