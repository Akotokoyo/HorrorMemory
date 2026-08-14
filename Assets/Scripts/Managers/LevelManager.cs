using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static event Action<bool> OnLevelEnded;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private Image distortedImage;
    [SerializeField] private Image originalImage;
    [SerializeField] private Image modifiedImage;
    [SerializeField] private RectTransform originalRect;
    [SerializeField] private RectTransform modifiedRect;
    [SerializeField] private ComparisonZoomPanController zoomPanController;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI differenceFoundText;

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

    public static LevelManager Instance
    {
        get { return _instance; }
    }

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
        timerText.color = Color.white;
        distortedImage.sprite = currentLevel.distortedSprite;
        originalImage.sprite = currentLevel.originalSprite;
        currentAlpha = 1f;
        totalDifferenceCount = 0;
        currentDifferenceCount = 0;

        modifiedImage.sprite = currentLevel.originalSprite;

        InitializeZoomPanController();
        zoomPanController?.ResetView();

        for(int i = 0; i < currentLevel.differences.Count; i++)
        {
            GameObject originalDiff = originalDifferences[i];
            if (!currentLevel.differences[i].mustBeFound)
            {
                originalDiff.SetActive(false);
                continue;
            }
            else
            {
                originalDiff.SetActive(true);
                originalDiff.GetComponent<Image>().sprite = currentLevel.differences[i].startedSprite;
                originalDiff.transform.GetChild(0).gameObject.SetActive(false);
                originalDiff.GetComponent<Difference>().diffInfo = currentLevel.differences[i];
                originalDiff.GetComponent<Difference>().diffIndex = i;
                originalDiff.GetComponent<Difference>().isClickable = true;
                originalDiff.GetComponent<Difference>().isFound = false;
            }

            RectTransform diffRect = originalDiff.GetComponent<RectTransform>();
            diffRect.sizeDelta = new Vector2
                (currentLevel.differences[i].width, currentLevel.differences[i].height);

            float width = originalRect.rect.width;
            float height = originalRect.rect.height;
            Vector2 normalized = currentLevel.differences[i].normalizedPosition;
            float x = (normalized.x - 0.5f) * width;
            float y = (normalized.y - 0.5f) * height;
            diffRect.anchoredPosition = new Vector2(x, y);
        }

        for (int i = 0; i < currentLevel.differences.Count; i++)
        {
            GameObject modDiff = differencesToFind[i];
            if (!currentLevel.differences[i].mustBeFound)
            {
                modDiff.SetActive(false);
                continue;
            }
            else
            {
                totalDifferenceCount++;
                modDiff.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                modDiff.SetActive(true);
            }

            modDiff.GetComponent<Image>().sprite = currentLevel.differences[i].startedSprite;
            modDiff.GetComponent<Difference>().diffInfo = currentLevel.differences[i];
            modDiff.transform.GetChild(0).gameObject.SetActive(false);
            modDiff.GetComponent<Difference>().diffIndex = i;
            modDiff.GetComponent<Difference>().isClickable = true;
            modDiff.GetComponent<Difference>().isFound = false;

            RectTransform diffRect = modDiff.GetComponent<RectTransform>();
            diffRect.sizeDelta = new Vector2
                (currentLevel.differences[i].width, currentLevel.differences[i].height);

            float width = modifiedRect.rect.width;
            float height = modifiedRect.rect.height;
            Vector2 normalized = currentLevel.differences[i].normalizedPosition;
            float x = (normalized.x - 0.5f) * width;
            float y = (normalized.y - 0.5f) * height;
            diffRect.anchoredPosition = new Vector2(x, y);
        }

        UpdateUI();
        updateTimerCoroutine = StartCoroutine(UpdateTimer());
    }

    public void OnDifferenceClicked(int diffIndex)
    {
        originalDifferences[diffIndex].GetComponent<Image>().sprite = originalDifferences[diffIndex].GetComponent<Difference>().diffInfo.distortedSprite;
        originalDifferences[diffIndex].GetComponent<Difference>().isFound = true;
        originalDifferences[diffIndex].transform.GetChild(0).gameObject.SetActive(true);
        differencesToFind[diffIndex].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        differencesToFind[diffIndex].GetComponent<Difference>().isFound = true;
        differencesToFind[diffIndex].transform.GetChild(0).gameObject.SetActive(true);
        currentDifferenceCount++;
        currentTimer += currentLevel.scoreAddTime;
        breakTime = 3f;
        UpdateUI();
        audioManager.StartEffectSound(currentLevel.completionSound);
        if (currentDifferenceCount == totalDifferenceCount)
        {
            OnLevelEnded?.Invoke(true);
            StopAllCoroutines();
            StartCoroutine(ShowEndPopup(true));
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
        differenceFoundText.text = $"{currentDifferenceCount}/{totalDifferenceCount}";        
    }

    private IEnumerator UpdateTimer()
    {
        while (currentTimer > 0)
        {
            currentTimer -= 1f;
            if(breakTime > 0)
            {
                breakTime -= 1f;
            }

            var timePlaying = TimeSpan.FromSeconds(currentTimer);
            timerText.text = timePlaying.ToString(@"mm\:ss");

            if(breakTime == 0)
            {
                currentAlpha = Mathf.Clamp01(currentTimer / currentLevel.timeLimit);
            }
            originalImage.color = new Color(1f, 1f, 1f, currentAlpha);

            if (currentTimer <= 5f)
            {
                timerText.color = Color.red;
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
        int starNumber = CalculateStarRating();
        if (levelSuccess)
        {
            GameManager.Instance.UpdateGameData(currentLevel.LevelId, starNumber, currentTimer);
        }
        bool isFinalLevel = levelSuccess && currentLevel.LevelId == Constants.LAST_LEVEL_INDEX;
        UIManager.Instance.ShowEndPopup(levelSuccess, currentTimer, starNumber, isFinalLevel);
    }

    private int CalculateStarRating() {
        if ((currentTimer < currentLevel.timeLimit && currentTimer >= currentLevel.timeLimit * Constants.FIRST_STAR_RANGE_PERCENTAGE) || currentTimer >= currentLevel.timeLimit) return 3;
        if (currentTimer < currentLevel.timeLimit * Constants.FIRST_STAR_RANGE_PERCENTAGE && currentTimer >= currentLevel.timeLimit * Constants.SECOND_STAR_RANGE_PERCENTAGE) return 2;
        if (currentTimer < currentLevel.timeLimit * Constants.SECOND_STAR_RANGE_PERCENTAGE && currentTimer > 0) return 1;
        return 0;
    }
}
