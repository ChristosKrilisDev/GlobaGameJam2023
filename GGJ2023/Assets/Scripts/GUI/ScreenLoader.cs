using DG.Tweening;
using UnityEngine;

public class ScreenLoader : MonoBehaviour
{
    [SerializeField]private CanvasGroup canvasGroup;

    private void Awake()
    {
        FadeOut();
    }

    private void FadeIn()
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
    
}
