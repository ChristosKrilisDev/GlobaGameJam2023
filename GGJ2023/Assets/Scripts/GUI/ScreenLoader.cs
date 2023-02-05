using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScreenLoader : MonoBehaviour
{
    [SerializeField]private CanvasGroup canvasGroup;
    [SerializeField]private TMP_Text gameDescription;

    
    
    private void Awake()
    {
        Debug.Log("TOM 1");
        
        
        if (SceneManager.GetActiveScene().name.Equals("MainScene"))
        {
            Vector3 startPos = new Vector3(0, 0, 0);
            Vector3 endPos = new Vector3(0, 0, 0);
            float timePassed = 0.0f, duration = 10.0f;
            bool hideText = false;
            
            if (GameController.Instance != null)
            {
                GameController.Instance.gameState = GameState.Transition;
            } 
            
            Tweener tweener = DOTween.To(() => startPos, x => startPos = x, endPos, duration)
                .OnStart(() =>
                {
                    canvasGroup.alpha = 1;
                    gameDescription.gameObject.SetActive(true);
                })
                .OnUpdate(() =>
                {
                    if (!hideText && timePassed > 8.0f)
                    {
                        gameDescription.gameObject.SetActive(false);
                        hideText = true;
                    }

                    timePassed += Time.deltaTime;
                })
                .OnComplete(() =>
                {
                    Debug.Log("TOM 2");
                    FadeOut();
                });
        }
        else
        {
            Debug.Log("TOM 3");
            FadeOut();
        }
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
            if (GameController.Instance != null)
            {
                GameController.Instance.gameState = GameState.Normal;
                GameController.Instance.GoToNextLevel();
            }
            
        });
    }

    public void LoadScene(int index)
    {
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(true);
        
        canvasGroup.DOKill();
        canvasGroup.DOFade(1f, 2f).SetEase(Ease.OutQuad).OnComplete(() =>
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
