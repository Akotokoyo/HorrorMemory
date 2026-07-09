using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject levelSelection;
    public GameObject contentMenu;
    public GameObject introUI;
    public GameObject gameUI;
    private int currentLevel;

    public void OnClickIntroButton(string action)
    {
        contentMenu.SetActive(false);
        switch (action)
        {
            case "Story":
                levelSelection.SetActive(true);
                break;
            case "Play":
                GameManager.Instance.StartGame(-1, true);
                levelSelection.SetActive(false);
                introUI.SetActive(false);
                gameUI.SetActive(true);

                Debug.Log("Play Button is clicked");
                break;
            case "Options":
                Debug.Log("Options Button is clicked");
                break;
            case "Close":
                Debug.Log("Close Button is clicked");
                break;
            default:
                contentMenu.SetActive(true);
                Debug.LogWarning("Action not binded");
                break;
        }
    }

    public void OnClickBackButton()
    {
        contentMenu.SetActive(true);
        levelSelection.SetActive(false);
    }

    public void OnClickPlayLevel(int idLevel)
    {
        currentLevel = idLevel;
        GameManager.Instance.StartGame(idLevel);
        levelSelection.SetActive(false);
        introUI.SetActive(false);
        gameUI.SetActive(true);
    }

    public void OnclickReturnToTitle()
    {
        introUI.SetActive(true);
        contentMenu.SetActive(true);
        gameUI.SetActive(false);
    }
    public void OnclickRetryLevel()
    {
        GameManager.Instance.StartGame(currentLevel);
        levelSelection.SetActive(false);
        introUI.SetActive(false);
        gameUI.SetActive(true);
    }

    public void OnClickPlayNextLevel()
    {
        currentLevel++;
        OnClickPlayLevel(currentLevel);
    }
}
