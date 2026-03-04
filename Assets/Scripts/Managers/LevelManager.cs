using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static event Action<bool> OnLevelEnded;

    [SerializeField] private PopupManager popupManager;

    [SerializeField] private Image distortedImage;
    [SerializeField] private Image originalImage;
    [SerializeField] private Image modifiedImage;
    [SerializeField] private RectTransform originalRect;
    [SerializeField] private RectTransform modifiedRect;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI differenceFoundText;

    public LevelData currentLevel;

    [SerializeField] private GameObject differencePrefab;
    [SerializeField] private List<GameObject> originalDifferences;
    [SerializeField] private List<GameObject> differencesToFind;

    private int totalDifferenceCount = 0;
    private int currentDifferenceCount = 0;

    private float currentTimer;
    private static LevelManager _instance;

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
    }

    public IEnumerator PrepareLevel()
    {
        yield return popupManager.ShowPreGamePopup(currentLevel);
    }

    public void InitLevel()
    {
        if(currentLevel == null)
        {
            Debug.LogError("There is not a currentLevel Loaded");
            return;
        }
        if (originalDifferences == null)
        {
            originalDifferences = new List<GameObject>();
        }
        if (differencesToFind == null)
        {
            differencesToFind = new List<GameObject>();
        }

        ClearOldData();

        currentTimer = currentLevel.timeLimit;
        distortedImage.sprite = currentLevel.distortedSprite;
        originalImage.sprite = currentLevel.originalSprite;

        modifiedImage.sprite = currentLevel.originalSprite;

        for(int i = 0; i< currentLevel.differences.Count; i++)
        {
            var newDiff = Instantiate(differencePrefab, originalRect);
            newDiff.name = $"Difference_{i}";
            newDiff.GetComponent<Image>().sprite = currentLevel.differences[i].startedSprite;
            newDiff.GetComponent<Difference>().diffInfo = currentLevel.differences[i];
            newDiff.GetComponent<Difference>().diffIndex = i;
            newDiff.GetComponent<Difference>().isClickable = false;
            
            RectTransform diffRect = newDiff.GetComponent<RectTransform>();
            diffRect.sizeDelta = new Vector2
                (currentLevel.differences[i].width, currentLevel.differences[i].height);

            float width = originalRect.rect.width;
            float height = originalRect.rect.height;
            Vector2 normalized = currentLevel.differences[i].normalizedPosition;
            float x = (normalized.x - 0.5f) * width;
            float y = (normalized.y - 0.5f) * height;
            diffRect.anchoredPosition = new Vector2(x, y);
            originalDifferences.Add(newDiff);
        }

        for (int i = 0; i < currentLevel.differences.Count; i++)
        {
            var newDiff = Instantiate(differencePrefab, modifiedRect);
            newDiff.name = $"Difference_{i}";
            newDiff.GetComponent<Image>().sprite = currentLevel.differences[i].startedSprite;
            newDiff.GetComponent<Difference>().diffInfo = currentLevel.differences[i];
            newDiff.GetComponent<Difference>().diffIndex = i;

            RectTransform diffRect = newDiff.GetComponent<RectTransform>();
            diffRect.sizeDelta = new Vector2
                (currentLevel.differences[i].width, currentLevel.differences[i].height);

            float width = modifiedRect.rect.width;
            float height = modifiedRect.rect.height;
            Vector2 normalized = currentLevel.differences[i].normalizedPosition;
            float x = (normalized.x - 0.5f) * width;
            float y = (normalized.y - 0.5f) * height;
            diffRect.anchoredPosition = new Vector2(x, y);
            if (currentLevel.differences[i].mustBeFound)
            {
                totalDifferenceCount++;
                newDiff.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            }
            differencesToFind.Add(newDiff);
        }

        UpdateUI();
        StartCoroutine(UpdateTimer());
    }

    public void OnDifferenceClicked(int diffIndex)
    {
        originalDifferences[diffIndex].GetComponent<Image>().sprite = originalDifferences[diffIndex].GetComponent<Difference>().diffInfo.distortedSprite;
        currentDifferenceCount++;
        float alpha = 1f - (currentDifferenceCount / (float)totalDifferenceCount);
        originalImage.color = new Color(1f, 1f, 1f, alpha);
        UpdateUI();
        if (currentDifferenceCount == totalDifferenceCount)
        {
            OnLevelEnded?.Invoke(true);
            StartCoroutine(ShowEndPopup(true));
        }
    }

    private void ClearOldData() { }
    private void UpdateUI() {
        differenceFoundText.text = $"{currentDifferenceCount}/{totalDifferenceCount}";        
    }

    private IEnumerator UpdateTimer()
    {
        while (currentTimer > 0)
        {
            currentTimer -= 1f;

            var timePlaying = TimeSpan.FromSeconds(currentTimer);
            timerText.text = timePlaying.ToString(@"mm\:ss");

            if (currentTimer <= 30f)
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
        popupManager.ShowEndPopup(true);
    }
}
