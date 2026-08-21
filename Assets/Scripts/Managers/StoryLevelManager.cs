using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Logica Story Mode, copiata da LevelManager per non toccare il Play.
/// Sinistra: Modified (oggetti gia' nell'immagine).
/// Destra: Original che nel tempo sbiadisce sul Distorted.
/// Le differenze restano solo hitbox invisibili: non si disegnano overlay started/distorted.
/// </summary>
public class StoryLevelManager
{
    private readonly LevelManager host;
    private Image cleanOverlay;

    public StoryLevelManager(LevelManager host)
    {
        this.host = host;
    }

    public void InitLevel()
    {
        ILevelData level = host.currentLevel;
        Sprite modified = level.modifiedSprite;
        if (modified == null)
        {
            Debug.LogError($"Story level {level.LevelId}: missing modifiedSprite. Fallback to originalSprite.");
            modified = level.originalSprite;
        }

        host.DistortedImage.sprite = modified;
        host.OriginalImage.enabled = false;
        host.OriginalImage.raycastTarget = false;

        host.ModifiedImage.sprite = level.distortedSprite;

        Image overlay = EnsureCleanOverlay();
        overlay.gameObject.SetActive(true);
        overlay.sprite = level.originalSprite;
        overlay.color = Color.white;
        host.CurrentAlpha = 1f;

        host.TotalDifferenceCount = 0;
        host.CurrentDifferenceCount = 0;

        for (int i = 0; i < level.differences.Count; i++)
        {
            DifferenceInfo info = level.differences[i];
            SetupInvisibleHitbox(host.OriginalDifferences[i], info, i, host.OriginalRect, countTowardsTotal: false);
            SetupInvisibleHitbox(host.DifferencesToFind[i], info, i, host.ModifiedRect, countTowardsTotal: true);
        }
    }

    public void OnDifferenceClicked(int diffIndex)
    {
        MarkFound(host.OriginalDifferences[diffIndex]);
        MarkFound(host.DifferencesToFind[diffIndex]);

        host.CurrentDifferenceCount++;
        host.BreakTime = 3f;
        host.NotifyDifferenceProgress();
        host.PlayCompletionSound();

        if (host.CurrentDifferenceCount == host.TotalDifferenceCount)
        {
            host.CompleteLevel(true);
        }
    }

    public void UpdateFade(float currentTimer, float timeLimit, float breakTime)
    {
        if (cleanOverlay == null || !cleanOverlay.gameObject.activeSelf)
        {
            return;
        }

        if (breakTime == 0)
        {
            host.CurrentAlpha = Mathf.Clamp01(currentTimer / timeLimit);
        }

        cleanOverlay.color = new Color(1f, 1f, 1f, host.CurrentAlpha);
    }

    public void HideCleanOverlay()
    {
        if (cleanOverlay != null)
        {
            cleanOverlay.gameObject.SetActive(false);
        }
    }

    private void SetupInvisibleHitbox(
        GameObject hitbox,
        DifferenceInfo info,
        int index,
        RectTransform parentRect,
        bool countTowardsTotal)
    {
        if (!info.mustBeFound)
        {
            hitbox.SetActive(false);
            return;
        }

        if (countTowardsTotal)
        {
            host.TotalDifferenceCount++;
        }

        hitbox.SetActive(true);
        Image image = hitbox.GetComponent<Image>();
        image.sprite = null;
        image.color = new Color(1f, 1f, 1f, 0f);
        image.raycastTarget = true;

        Difference difference = hitbox.GetComponent<Difference>();
        difference.diffInfo = info;
        difference.diffIndex = index;
        difference.isClickable = true;
        difference.isFound = false;
        hitbox.transform.GetChild(0).gameObject.SetActive(false);

        LevelManager.PlaceDifference(hitbox, info, parentRect);
    }

    private static void MarkFound(GameObject hitbox)
    {
        hitbox.GetComponent<Difference>().isFound = true;
        hitbox.transform.GetChild(0).gameObject.SetActive(true);
    }

    private Image EnsureCleanOverlay()
    {
        if (cleanOverlay != null)
        {
            return cleanOverlay;
        }

        GameObject go = new GameObject("StoryCleanOverlay", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.SetParent(host.ModifiedImage.rectTransform, false);
        rect.SetAsFirstSibling();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        rect.localScale = Vector3.one;

        cleanOverlay = go.GetComponent<Image>();
        cleanOverlay.raycastTarget = false;
        cleanOverlay.preserveAspect = false;
        return cleanOverlay;
    }
}
