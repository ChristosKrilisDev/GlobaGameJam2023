using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScreenLoader : MonoBehaviour
{
    [SerializeField]private CanvasGroup canvasGroup;

    
    
    private void Awake()
    {
        FadeOut();
    }

    private void FadeIn(Action action)
    {
        canvasGroup.DOKill();
        canvasGroup.DOFade(1f, 5f).SetEase(Ease.InCubic);
    }

    private void FadeOut()
    {
        canvasGroup.alpha = 1;
        canvasGroup.DOKill();
        canvasGroup.DOFade(0f, 5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            canvasGroup.gameObject.SetActive(false);
        });
    }

    public void LoadScene(int index)
    {
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(true);
        
        canvasGroup.DOKill();
        canvasGroup.DOFade(1f, 2.5f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            SceneManager.LoadScene(index);
        });
        
    }
    
}
