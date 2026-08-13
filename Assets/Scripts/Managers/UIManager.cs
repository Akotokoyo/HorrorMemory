using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Popups")]
    public GameObject contentMenu;
    public GameObject introUI;
    public GameObject gameUI;
    [SerializeField] private GameObject pausePopup;
    [SerializeField] private GameObject endGamePopup;
    [SerializeField] private TextMeshProUGUI endGameText;

    [Header("Tutorial")]
    [SerializeField] private GameObject tutorialPopup;
    [SerializeField] private GameObject tutorialNextButton;
    [SerializeField] private TextMeshProUGUI tutorialTitleText;
    [SerializeField] private TextMeshProUGUI tutorialDescriptionText;
    [SerializeField] private GameObject tutorialLeftImage;
    [SerializeField] private GameObject tutorialRightImage;
    [SerializeField] private Image tutorialFlagButtonImage;
    [SerializeField] private List<Sprite> tutorialSprites;
    private int tutorialStepIndex = 0;

    private int currentLevel;
    [Header("Miscellaneous")]
    public GameObject levelSelection;
    public List<GameObject> levelPrefabs;
    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Image flagButtonImage;
    [SerializeField] private List<Sprite> flagSprites;

    private static UIManager _instance;

    public static UIManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        int languageIndex = PlayerPrefs.GetInt("LanguageId", 0);
        flagButtonImage.sprite = flagSprites[languageIndex];
        tutorialFlagButtonImage.sprite = flagSprites[languageIndex];

        if (PlayerPrefs.GetInt("TutorialSeen", 0) == 0)
        {
            OnClickIntroButton("Tutorial");
        }
    }

    public void GenerateLevelPrefabs(GameData gameData)
    {
        for(int i = 0; i < gameData.Levels.Count; i++)
        {
            int levelIndex = i;
            GameObject go = Instantiate(levelPrefab, scrollViewContent.transform);
            go.GetComponent<Button>().onClick.AddListener(() => OnClickPlayLevel(levelIndex));
            levelPrefabs.Add(go);
        }
    }

    public void OnClickIntroButton(string action)
    {
        contentMenu.SetActive(false);
        switch (action)
        {
            case "Story":
                StartCoroutine(PrepareLevelsFromGameData());
                levelSelection.SetActive(true);
                break;
            case "Tutorial":
                SetTutorialStep0(); 
                tutorialPopup.SetActive(true);
                break;
            case "Play":
                GameManager.Instance.StartGame(-1, true);
                levelSelection.SetActive(false);
                introUI.SetActive(false);
                gameUI.SetActive(true);
                Debug.Log("Play Button is clicked");
                break;
            case "Close":
                Application.Quit();
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
        pausePopup.SetActive(false);
        gameUI.SetActive(true);
    }

    public void OnclickReturnToTitle()
    {
        introUI.SetActive(true);
        contentMenu.SetActive(true);
        gameUI.SetActive(false);
        pausePopup.SetActive(false);
        endGamePopup.SetActive(false);
    }
    public void OnclickRetryLevel()
    {
        GameManager.Instance.StartGame(currentLevel);
        levelSelection.SetActive(false);
        introUI.SetActive(false);
        pausePopup.SetActive(false);
        gameUI.SetActive(true);
    }

    public void OnClickPlayNextLevel()
    {
        currentLevel++;
        OnClickPlayLevel(currentLevel);
    }

    public void OnClickPauseGameButton(bool isPaused)
    {
        LevelManager.Instance.PauseGame(isPaused);
        pausePopup.SetActive(isPaused);
    }

    public void OnClickEndingGameButton(bool isGoodEnding)
    {
        endGamePopup.SetActive(true);
        endGameText.text = 
            LanguageManager.Instance.TranslateText(
                isGoodEnding ? "STORY_LEVEL_019_LIFE_OUTRO_TEXT_ID" : "STORY_LEVEL_019_DEATH_OUTRO_TEXT_ID");
    }

    public void OnClickChangeLanguage()
    {
        int languageIndex = PlayerPrefs.GetInt("LanguageId", 0);
        languageIndex++;
        if(languageIndex == Constants.MAX_LANGUAGES)
        {
            languageIndex = 0;
        }
        flagButtonImage.sprite = flagSprites[languageIndex];
        tutorialFlagButtonImage.sprite = flagSprites[languageIndex];
        LanguageManager.Instance.ChangeLanguage(languageIndex);
    }

    private IEnumerator PrepareLevelsFromGameData()
    {
        for(int i = 0; i < levelPrefabs.Count; i++)
        {
            Level level = GameManager.Instance.GetLevelFromGameData(i);
            if (!level.IsAvailable)
            {
                levelPrefabs[i].GetComponent<Button>().interactable = false;
                levelPrefabs[i].transform.GetChild(0).gameObject.SetActive(true);
                levelPrefabs[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                levelPrefabs[i].GetComponent<Button>().interactable = true;
                levelPrefabs[i].transform.GetChild(0).gameObject.SetActive(false);
                levelPrefabs[i].transform.GetChild(1).gameObject.SetActive(true);
                Transform tr = levelPrefabs[i].transform.GetChild(1);
                tr.GetChild(0).GetComponent<TranslateTexts>().ChangeTextByScript(level.LevelName);
                
                var op = Addressables.LoadAssetAsync<Sprite>(level.AddrImage);
                yield return op;
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    tr.GetChild(1).GetComponent<Image>().sprite = op.Result;
                }

                var timePlaying = TimeSpan.FromSeconds(level.BestTimer);
                tr.GetChild(2).GetComponent<TextMeshProUGUI>().text = timePlaying.ToString(@"mm\:ss");
                for(int j = 0; j < 3; j++)
                {
                    tr.GetChild(3).GetChild(j).GetChild(0).gameObject.SetActive(j < level.StarRating);
                }
            }
        } 
    }

    #region Tutorial
    private void SetTutorialStep0()
    {
        tutorialStepIndex = 0;
        tutorialTitleText.GetComponent<TranslateTexts>().ChangeTextByScript($"TUTORIAL_STEP_{tutorialStepIndex}_TITLE_TEXT_ID");
        tutorialDescriptionText.GetComponent<TranslateTexts>().ChangeTextByScript($"TUTORIAL_STEP_{tutorialStepIndex}_DESCRIPTION_TEXT_ID");
        tutorialLeftImage.GetComponent<Image>().sprite = tutorialSprites[0];
        tutorialRightImage.GetComponent<Image>().sprite = tutorialSprites[0];
        tutorialLeftImage.transform.GetChild(0).gameObject.SetActive(true);
        tutorialLeftImage.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        tutorialNextButton.SetActive(false);
    }
    public void OnClickTutorialDifference()
    {
        tutorialLeftImage.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        tutorialRightImage.transform.GetChild(0).gameObject.SetActive(true);
        tutorialRightImage.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        tutorialNextButton.SetActive(true);
    }
    public void OnClickTutorialNextButton()
    {
        tutorialStepIndex++;
        tutorialTitleText.GetComponent<TranslateTexts>().ChangeTextByScript($"TUTORIAL_STEP_{tutorialStepIndex}_TITLE_TEXT_ID");
        tutorialDescriptionText.GetComponent<TranslateTexts>().ChangeTextByScript($"TUTORIAL_STEP_{tutorialStepIndex}_DESCRIPTION_TEXT_ID");
        switch (tutorialStepIndex)
        {
            case 1:
                tutorialLeftImage.transform.GetChild(0).gameObject.SetActive(false);
                tutorialRightImage.transform.GetChild(0).gameObject.SetActive(false);
                tutorialLeftImage.GetComponent<Image>().sprite = tutorialSprites[1];
                break;
            case 2:
                tutorialLeftImage.GetComponent<Image>().sprite = tutorialSprites[2];
                tutorialRightImage.GetComponent<Image>().sprite = tutorialSprites[3];
                break;
            case 3:
                contentMenu.SetActive(true);
                tutorialPopup.SetActive(false);
                PlayerPrefs.SetInt("TutorialSeen", 1);
                break;
            default:
                break;
        }
    }

    #endregion
}
