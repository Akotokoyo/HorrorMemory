using UnityEngine;

public class IntroMenu : MonoBehaviour
{
    public GameObject levelSelection;
    public GameObject contentMenu;

    public void OnClickIntroButton(string action)
    {
        contentMenu.SetActive(false);
        switch (action)
        {
            case "Story":
                levelSelection.SetActive(true);
                Debug.Log("Story Button is clicked");
                break;
            case "Play":
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
}
