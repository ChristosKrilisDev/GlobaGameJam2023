using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLoader : MonoBehaviour
{
    [SerializeField]private CanvasGroup _canvasGroup;
    [SerializeField]private TMP_Text _gameDescription;

    private void Awake()
    {
        FadeOut(_gameDescription);
    }

    private void FadeIn(Action action)
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1f, 5f).SetEase(Ease.InCubic);
    }

    private void FadeOut(TMP_Text tmpText = null)
    {
        GameController.Instance.Init();

        if (tmpText != null)
        {
            tmpText.gameObject.SetActive(true);
        }
        
        _canvasGroup.alpha = 1;
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0f, 3f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            _canvasGroup.gameObject.SetActive(false);
            if (tmpText != null)
            {
                tmpText.gameObject.SetActive(false);
            }
            if (GameController.Instance != null)
            {
                GameController.Instance.gameState = GameState.Normal;
            }
            
        });
    }

    public void LoadScene(int index )
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.gameObject.SetActive(true);
        
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1f, 2f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            SceneManager.LoadScene(index);

            if (GameController.Instance != null)
            {
                GameController.Instance.gameState = GameState.Normal;
                GameController.Instance.GoToNextLevel();
            }
        });
        
    }
    
}
