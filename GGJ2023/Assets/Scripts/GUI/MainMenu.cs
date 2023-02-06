using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject _mainMenuGui;
    [SerializeField] private GameObject _creditsGui;
    
    
    private void Start()
    {
        OpenMainMenu();
    }

    public void OpenCredit()
    {
        _mainMenuGui.gameObject.SetActive(false);
        _creditsGui.gameObject.SetActive(true);
    }

    public void OpenMainMenu()
    {
        _mainMenuGui.gameObject.SetActive(true);
        _creditsGui.gameObject.SetActive(false);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
