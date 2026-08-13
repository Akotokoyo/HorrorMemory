using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{

    [Header("Pre Game Popup")]
    [SerializeField] private GameObject preGamePopup;
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private TextMeshProUGUI levelTimerText;
    [SerializeField] private TextMeshProUGUI levelDifficultyText;
    [SerializeField] private TextMeshProUGUI levelWaitTimeText;
    [SerializeField] private TextMeshProUGUI levelStoryInfoText;

    [Header("Ending Popup")]
    [SerializeField] private GameObject endPopup;
    [SerializeField] private TextMeshProUGUI endTitleText;
    [SerializeField] private TextMeshProUGUI endLevelTimeLeftInfoText;
    [SerializeField] private TextMeshProUGUI endLevelStoryInfoText;
    [SerializeField] private List<GameObject> stars;

    [SerializeField] private GameObject homeButton;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject endGameGoodButton;
    [SerializeField] private GameObject endGameBadButton;

    private string endLevelStoryInfo;
    private int waitingtime;

    public void ShowPreGamePopup(ILevelData level)
    {
        preGamePopup.SetActive(true);
        levelNameText.GetComponent<TranslateTexts>().ChangeTextByScript(level.LevelDisplayName);
        levelTimerText.GetComponent<TranslateTexts>().ChangeTextAndReplaceVariables("PRE_GAME_TIMER_TEXT_ID", new List<string> { level.timeLimit.ToString() });
        levelDifficultyText.GetComponent<TranslateTexts>().ChangeTextByScript(GetTextIdFromDifficulty(level.difficulty));
        levelStoryInfoText.GetComponent<TranslateTexts>().ChangeTextByScript(level.storyIntroText);
        endLevelStoryInfo = level.storyEndingText;        
    }

    public void OnClickStartLevel()
    {
        preGamePopup.SetActive(false);
        GameManager.Instance.gameState = GameState.PLAYING;
        LevelManager.Instance.InitLevel();
    }

    public void ShowEndPopup(bool levelSuccess, float remainingTime, int starNumber)
    {
        endPopup.SetActive(true);
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].SetActive((i < starNumber) ? true : false);
        }

        if (LevelManager.Instance.currentLevel.LevelId == Constants.LAST_LEVEL_INDEX
            && levelSuccess)
        {
            retryButton.SetActive(false);
            homeButton.SetActive(false);
            nextLevelButton.SetActive(false);
            endGameGoodButton.SetActive(true);
            endGameBadButton.SetActive(true);

            endTitleText.GetComponent<TranslateTexts>().
                ChangeTextByScript("FINAL_CHOICE_TEXT_ID");
            endLevelTimeLeftInfoText.GetComponent<TranslateTexts>().
                ChangeTextAndReplaceVariables("LEVEL_REMAINING_TIME_TEXT_ID", new List<string> { remainingTime.ToString() });
        }
        else
        {
            endTitleText.text =
                LanguageManager.Instance.TranslateText(levelSuccess ? "LEVEL_SUCCESS_TEXT_ID" : "LEVEL_FAILED_TEXT_ID");
            endLevelTimeLeftInfoText.GetComponent<TranslateTexts>().
                ChangeTextAndReplaceVariables("LEVEL_REMAINING_TIME_TEXT_ID", new List<string> { remainingTime.ToString() });

            endLevelStoryInfoText.GetComponent<TranslateTexts>().
                ChangeTextByScript(levelSuccess ? endLevelStoryInfo : "");
            retryButton.SetActive(true);
            homeButton.SetActive(true);
            endGameGoodButton.SetActive(false);
            endGameBadButton.SetActive(false);

            nextLevelButton.SetActive(levelSuccess);
        }
    }

    public void HideEndPopup()
    {
        endPopup.SetActive(false);
    }

    private string GetTextIdFromDifficulty(DifficultyLevel diff)
    {
        switch (diff)
        {
            case DifficultyLevel.Easy:
                return "DIFFICULTY_LEVEL_EASY_TEXT_ID";
            case DifficultyLevel.Medium:
                return "DIFFICULTY_LEVEL_MEDIUM_TEXT_ID";
            case DifficultyLevel.Hard:
                return "DIFFICULTY_LEVEL_HARD_TEXT_ID";
            default:
                return "DIFFICULTY_LEVEL_EASY_TEXT_ID";
        }
    }
}
