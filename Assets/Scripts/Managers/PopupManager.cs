using System;
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

    private int waitingtime;

    public IEnumerator ShowPreGamePopup(LevelData level)
    {
        preGamePopup.SetActive(true);
        levelNameText.text = level.name;
        levelTimerText.text = $"Time Limit: {level.timeLimit} seconds";
        levelDifficultyText.text = $"Difficulty: {level.difficulty}";
        levelStoryInfoText.text = level.storyIntroText;
        waitingtime = level.waitingtime;
        while (waitingtime != 0)
        {
            levelWaitTimeText.text = $"The Game will start after: {waitingtime} seconds";
            yield return new WaitForSeconds(1f);
            waitingtime--;
        }
        
        preGamePopup.SetActive(false);
    }
}
