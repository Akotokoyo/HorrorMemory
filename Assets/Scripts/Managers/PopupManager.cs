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
    [SerializeField] private GameObject nextLevelButton;
    
    private string endLevelStoryInfo;
    private int waitingtime;

    public IEnumerator ShowPreGamePopup(ILevelData level)
    {
        preGamePopup.SetActive(true);
        levelNameText.GetComponent<TranslateTexts>().ChangeTextByScript(level.LevelDisplayName);
        levelTimerText.GetComponent<TranslateTexts>().ChangeTextAndReplaceVariables("PRE_GAME_TIMER_TEXT_ID", new List<string> { level.timeLimit.ToString() });
        string difficultyTextId = GetTextIdFromDifficulty(level.difficulty);
        levelDifficultyText.text = LanguageManager.Instance.TranslateText("PRE_GAME_DIFFICULTY_TEXT_ID") + LanguageManager.Instance.TranslateText(difficultyTextId);
        levelStoryInfoText.GetComponent<TranslateTexts>().ChangeTextByScript(level.storyIntroText);
        endLevelStoryInfo = level.storyEndingText;
        waitingtime = level.waitingtime;
        while (waitingtime != 0)
        {
            levelWaitTimeText.GetComponent<TranslateTexts>().ChangeTextAndReplaceVariables("PRE_GAME_TIME_TO_START_TEXT_ID", new List<string> { waitingtime.ToString() });
            yield return new WaitForSeconds(1f);
            waitingtime--;
        }
        
        preGamePopup.SetActive(false);
    }

    public void ShowEndPopup(bool levelSuccess, float remainingTime, int starNumber)
    {
        endPopup.SetActive(true);
        endTitleText.text = 
            LanguageManager.Instance.TranslateText(levelSuccess ? "LEVEL_SUCCESS_TEXT_ID" : "LEVEL_FAILED_TEXT_ID");
        endLevelTimeLeftInfoText.GetComponent<TranslateTexts>().
            ChangeTextAndReplaceVariables("LEVEL_REMAINING_TIME_TEXT_ID", new List<string> { remainingTime.ToString() });
        endLevelStoryInfoText.text = levelSuccess ? endLevelStoryInfo : "";
        nextLevelButton.SetActive(levelSuccess);

        for (int i = 0; i< stars.Count; i++)
        {
            stars[i].SetActive((i < starNumber) ? true : false);
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
