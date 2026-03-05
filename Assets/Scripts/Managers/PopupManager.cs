using System.Collections;
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
    [SerializeField] private TextMeshProUGUI endLevelStoryInfoText;


    private string endLevelStoryInfo;
    private int waitingtime;

    public IEnumerator ShowPreGamePopup(LevelData level)
    {
        preGamePopup.SetActive(true);
        levelNameText.text = level.name;
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

    public void ShowEndPopup(bool levelSuccess)
    {
        endPopup.SetActive(true);
        endTitleText.text = levelSuccess ? "Level Completed!" : "Level Failed!";
        endLevelStoryInfoText.text = levelSuccess ? endLevelStoryInfo : "";
    }
}
