using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagers : MonoBehaviour
{
    public static MenuManagers _;
    [SerializeField] private bool _debugMode;
    [SerializeField] private string _SceneToLoad;
    public enum MainMenuButtons { Start, options, saves, quit };

    public void Awake()
    {
        if (_ == null)
        {
            _ = this;
        }
        else
        {
            Debug.Log("more than one Menumanagers script present");
        }
    }
    public void MainMenuButtonsClicked(MainMenuButtons buttonClicked)
    {
        DebugMessage("Clicked  " + buttonClicked.ToString(""));
        switch (buttonClicked)
        {
            case MainMenuButtons.Start:
                PlayClicked();
                break;
            case MainMenuButtons.options:
                break;
            case MainMenuButtons.saves:
                break;
            case MainMenuButtons.quit:
                QuitGame();

                break;
            default:
                Debug.Log("method not in the list");
                break;
        }
    }

    public void PlayClicked()
    {
        SceneManager.LoadScene(_SceneToLoad);
    }

    private void DebugMessage(string message)
    {
        Debug.Log(message);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.quit();
#endif
    }
}
