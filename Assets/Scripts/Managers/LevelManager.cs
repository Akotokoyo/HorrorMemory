using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static event Action<bool> OnLevelEnded;
    public static event Action<TimeSpan, bool> OnTimerUpdate;
    public static event Action<int, int> OnDifferenceProgress;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private Image distortedImage;
    [SerializeField] private Image originalImage;
    [SerializeField] private Image modifiedImage;
    [SerializeField] private RectTransform originalRect;
    [SerializeField] private RectTransform modifiedRect;
    [SerializeField] private ComparisonZoomPanController zoomPanController;

    public ILevelData currentLevel;

    [SerializeField] private GameObject differencePrefab;
    [SerializeField] private List<GameObject> originalDifferences;
    [SerializeField] private List<GameObject> differencesToFind;

    private int totalDifferenceCount = 0;
    private int currentDifferenceCount = 0;

    Coroutine updateTimerCoroutine = null;

    private float currentTimer;
    private float breakTime;
    private float currentAlpha;
    private static LevelManager _instance;
    private Transform comparisonRoot;
    private StoryLevelManager storyLevelManager;

    public static LevelManager Instance
    {
        get { return _instance; }
    }

    internal Image DistortedImage => distortedImage;
    internal Image OriginalImage => originalImage;
    internal Image ModifiedImage => modifiedImage;
    internal RectTransform OriginalRect => originalRect;
    internal RectTransform ModifiedRect => modifiedRect;
    internal List<GameObject> OriginalDifferences => originalDifferences;
    internal List<GameObject> DifferencesToFind => differencesToFind;

    internal int TotalDifferenceCount { get => totalDifferenceCount; set => totalDifferenceCount = value; }
    internal int CurrentDifferenceCount { get => currentDifferenceCount; set => currentDifferenceCount = value; }
    internal float BreakTime { get => breakTime; set => breakTime = value; }
    internal float CurrentAlpha { get => currentAlpha; set => currentAlpha = value; }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        comparisonRoot = distortedImage != null ? distortedImage.transform.parent : null;
        storyLevelManager = new StoryLevelManager(this);
        GenerateDiffsTemplate();
    }

    private void InitializeZoomPanController()
    {
        if (distortedImage == null || modifiedImage == null)
        {
            return;
        }

        // The zoom controller reparents the images into its viewports. Keep using
        // the original GameBackground instead of their current runtime parent.
        if (comparisonRoot == null)
        {
            comparisonRoot = distortedImage.transform.parent;
        }

        GameObject gameBackground = comparisonRoot.gameObject;

        if (zoomPanController == null || zoomPanController.gameObject != gameBackground)
        {
            zoomPanController = gameBackground.GetComponent<ComparisonZoomPanController>();
        }

        if (zoomPanController == null)
        {
            zoomPanController = gameBackground.AddComponent<ComparisonZoomPanController>();
        }

        zoomPanController.Setup(distortedImage.rectTransform, modifiedImage.rectTransform);
    }

    public void PrepareLevel()
    {
        UIManager.Instance.HideAllPopups();
        UIManager.Instance.ShowPreGamePopup(currentLevel);
    }

    public void StopLevel()
    {
        StopAllCoroutines();
        updateTimerCoroutine = null;
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            StopCoroutine(updateTimerCoroutine);
        }
        else
        {
            updateTimerCoroutine = StartCoroutine(UpdateTimer());
        }
    }

    public void InitLevel()
    {
        if(currentLevel == null)
        {
            Debug.LogError("There is not a currentLevel Loaded");
            return;
        }

        currentTimer = currentLevel.timeLimit;
        totalDifferenceCount = 0;
        currentDifferenceCount = 0;
        breakTime = 0f;
        currentAlpha = 1f;

        InitializeZoomPanController();
        zoomPanController?.ResetView();

        if (GameManager.Instance.IsCasualMode)
        {
            InitPlayLevel();
        }
        else
        {
            storyLevelManager.InitLevel();
        }

        UpdateUI();
        updateTimerCoroutine = StartCoroutine(UpdateTimer());
    }

    public void OnDifferenceClicked(int diffIndex)
    {
        if (GameManager.Instance.IsCasualMode)
        {
            OnPlayDifferenceClicked(diffIndex);
        }
        else
        {
            storyLevelManager.OnDifferenceClicked(diffIndex);
        }
    }

    internal void NotifyDifferenceProgress()
    {
        UpdateUI();
    }

    internal void PlayCompletionSound()
    {
        audioManager.StartEffectSound(currentLevel.completionSound);
    }

    internal void CompleteLevel(bool success)
    {
        OnLevelEnded?.Invoke(success);
        StopAllCoroutines();
        StartCoroutine(ShowEndPopup(success));
    }

    internal static void PlaceDifference(GameObject differenceObject, DifferenceInfo info, RectTransform parentRect)
    {
        RectTransform diffRect = differenceObject.GetComponent<RectTransform>();
        diffRect.sizeDelta = new Vector2(info.width, info.height);

        float width = parentRect.rect.width;
        float height = parentRect.rect.height;
        Vector2 normalized = info.normalizedPosition;
        diffRect.anchoredPosition = new Vector2(
            (normalized.x - 0.5f) * width,
            (normalized.y - 0.5f) * height);
    }

    private void InitPlayLevel()
    {
        storyLevelManager.HideCleanOverlay();

        distortedImage.sprite = currentLevel.originalSprite;
        originalImage.enabled = true;
        originalImage.sprite = currentLevel.originalSprite;
        originalImage.color = Color.white;
        originalImage.raycastTarget = true;
        modifiedImage.sprite = currentLevel.originalSprite;

        for (int i = 0; i < currentLevel.differences.Count; i++)
        {
            SetupPlayDifference(
                originalDifferences[i],
                currentLevel.differences[i],
                i,
                originalRect,
                visible: true,
                countTowardsTotal: false);
        }

        for (int i = 0; i < currentLevel.differences.Count; i++)
        {
            SetupPlayDifference(
                differencesToFind[i],
                currentLevel.differences[i],
                i,
                modifiedRect,
                visible: false,
                countTowardsTotal: true);
        }
    }

    private void SetupPlayDifference(
        GameObject differenceObject,
        DifferenceInfo info,
        int index,
        RectTransform parentRect,
        bool visible,
        bool countTowardsTotal)
    {
        if (!info.mustBeFound)
        {
            differenceObject.SetActive(false);
            return;
        }

        if (countTowardsTotal)
        {
            totalDifferenceCount++;
        }

        differenceObject.SetActive(true);
        Image image = differenceObject.GetComponent<Image>();
        image.sprite = info.startedSprite;
        image.color = visible ? Color.white : new Color(1f, 1f, 1f, 0f);
        image.raycastTarget = true;

        Difference difference = differenceObject.GetComponent<Difference>();
        difference.diffInfo = info;
        difference.diffIndex = index;
        difference.isClickable = true;
        difference.isFound = false;
        differenceObject.transform.GetChild(0).gameObject.SetActive(false);

        PlaceDifference(differenceObject, info, parentRect);
    }

    private void OnPlayDifferenceClicked(int diffIndex)
    {
        originalDifferences[diffIndex].GetComponent<Difference>().isFound = true;
        originalDifferences[diffIndex].transform.GetChild(0).gameObject.SetActive(true);
        differencesToFind[diffIndex].GetComponent<Image>().color = Color.white;
        differencesToFind[diffIndex].GetComponent<Difference>().isFound = true;
        differencesToFind[diffIndex].transform.GetChild(0).gameObject.SetActive(true);
        currentDifferenceCount++;
        UpdateUI();
        audioManager.StartEffectSound(currentLevel.completionSound);
        if (currentDifferenceCount == totalDifferenceCount)
        {
            CompleteLevel(true);
        }
    }

    private void GenerateDiffsTemplate()
    {
        if (originalDifferences == null)
        {
            originalDifferences = new List<GameObject>();
        }
        if (differencesToFind == null)
        {
            differencesToFind = new List<GameObject>();
        }

        for (int i = 0; i < Constants.MAX_DIFFERENCES; i++)
        {
            var origDiff = Instantiate(differencePrefab, originalRect);
            origDiff.name = $"OrigDifference_{i}";
            originalDifferences.Add(origDiff);

            var modDiff = Instantiate(differencePrefab, modifiedRect);
            modDiff.name = $"ModDifference_{i}";
            differencesToFind.Add(modDiff);
        }

    }
    private void UpdateUI() {
        OnDifferenceProgress?.Invoke(currentDifferenceCount, totalDifferenceCount);
    }

    private IEnumerator UpdateTimer()
    {
        bool isPlayMode = GameManager.Instance.IsCasualMode;

        while (currentTimer > 0)
        {
            currentTimer -= 1f;
            if(breakTime > 0)
            {
                breakTime -= 1f;
            }

            var timePlaying = TimeSpan.FromSeconds(currentTimer);
            OnTimerUpdate?.Invoke(timePlaying, currentTimer <= 5f);

            if (!isPlayMode)
            {
                storyLevelManager.UpdateFade(currentTimer, currentLevel.timeLimit, breakTime);
            }

            yield return new WaitForSeconds(1f);
        }

        if (currentTimer == 0)
        {
            OnLevelEnded?.Invoke(false);
            StartCoroutine(ShowEndPopup(false));
        }
    }

    private IEnumerator ShowEndPopup(bool levelSuccess)
    {
        yield return new WaitForSeconds(1f);
        int starNumber = GeneralFunctions.CalculateStarRating(currentLevel, currentTimer);
        bool isStoryMode = !GameManager.Instance.IsCasualMode;
        if (levelSuccess && isStoryMode)
        {
            GameManager.Instance.UpdateGameData(currentLevel.LevelId, starNumber, currentTimer);
        }
        bool isFinalLevel = levelSuccess && isStoryMode && currentLevel.LevelId == Constants.LAST_LEVEL_INDEX;
        UIManager.Instance.ShowEndPopup(levelSuccess, currentTimer, starNumber, isFinalLevel);
    }

    
}
