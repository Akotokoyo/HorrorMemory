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


    private string endLevelStoryInfo;
    private int waitingtime;

    public IEnumerator ShowPreGamePopup(ILevelData level)
    {
        preGamePopup.SetActive(true);
        levelNameText.text = level.LevelDisplayName;
        levelTimerText.text = $"Time Limit: {level.timeLimit} seconds";
        levelDifficultyText.text = $"Difficulty: {level.difficulty}";
        levelStoryInfoText.text = level.storyIntroText;
        endLevelStoryInfo = level.storyEndingText;
        waitingtime = level.waitingtime;
        while (waitingtime != 0)
        {
            levelWaitTimeText.text = $"The Game will start after: {waitingtime} seconds";
            yield return new WaitForSeconds(1f);
            waitingtime--;
        }
        
        preGamePopup.SetActive(false);
    }

    public void ShowEndPopup(bool levelSuccess, float remainingTime, int starNumber)
    {
        endPopup.SetActive(true);
        endTitleText.text = levelSuccess ? "Level Completed!" : "Level Failed!";
        endLevelTimeLeftInfoText.text = $"Remaining Time: {remainingTime}";
        endLevelStoryInfoText.text = levelSuccess ? endLevelStoryInfo : "";

        for(int i = 0; i< stars.Count; i++)
        {
            stars[i].SetActive((i < starNumber) ? true : false);
        }
    }
}
