using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class CenterFocusScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [Header("Focus Scaling")]
    [Tooltip("Size of the card when it is exactly in the center of the viewport.")]
    [SerializeField] private float maxScale = 1f;

    [Tooltip("Size of the cards that are far away from the center.")]
    [SerializeField] private float minScale = 0.8f;

    [Tooltip("Horizontal distance (in pixels) from the center at which a card is back to minScale.")]
    [SerializeField] private float falloffRange = 500f;

    [Tooltip("Falloff shape. X = normalized distance from center (0 = center, 1 = edge). Y = focus weight (1 = full grow, 0 = normal).")]
    [SerializeField] private AnimationCurve scaleByDistance = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

    [Tooltip("How quickly a card eases toward its target size. Higher = snappier.")]
    [SerializeField] private float smoothSpeed = 12f;

    [Header("Snap To Center")]
    [SerializeField] private bool enableSnap = true;

    [Tooltip("How quickly the content slides so the nearest card lands in the center.")]
    [SerializeField] private float snapSpeed = 12f;

    [Tooltip("Stop snapping once the nearest card is within this many pixels of the center.")]
    [SerializeField] private float snapStopThreshold = 0.5f;

    private ScrollRect scrollRect;
    private RectTransform viewport;
    private RectTransform content;

    private bool isDragging;
    private bool hasSnapTarget;
    private float snapTargetX;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        viewport = scrollRect.viewport != null ? scrollRect.viewport : (RectTransform)transform;
        content = scrollRect.content;
    }

    private void LateUpdate()
    {
        if (content == null)
        {
            return;
        }

        UpdateScales();

        if (enableSnap && !isDragging && hasSnapTarget)
        {
            UpdateSnap();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        hasSnapTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        scrollRect.velocity = Vector2.zero;
        ComputeSnapTarget();
    }

    private void UpdateScales()
    {
        float smoothing = 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime);

        for (int i = 0; i < content.childCount; i++)
        {
            RectTransform card = content.GetChild(i) as RectTransform;
            if (card == null)
            {
                continue;
            }

            float distance = Mathf.Abs(SignedDistanceToCenter(card));
            float normalized = falloffRange <= 0f ? 1f : Mathf.Clamp01(distance / falloffRange);
            float weight = scaleByDistance.Evaluate(normalized);
            float target = Mathf.Lerp(minScale, maxScale, weight);

            Vector3 targetScale = new Vector3(target, target, 1f);
            card.localScale = Vector3.Lerp(card.localScale, targetScale, smoothing);
        }
    }

    private void UpdateSnap()
    {
        Vector2 pos = content.anchoredPosition;
        float newX = Mathf.Lerp(pos.x, snapTargetX, 1f - Mathf.Exp(-snapSpeed * Time.deltaTime));

        if (Mathf.Abs(snapTargetX - newX) <= snapStopThreshold)
        {
            newX = snapTargetX;
            hasSnapTarget = false;
        }

        content.anchoredPosition = new Vector2(newX, pos.y);
    }

    private void ComputeSnapTarget()
    {
        RectTransform nearest = null;
        float nearestSigned = 0f;
        float nearestDistance = float.MaxValue;

        for (int i = 0; i < content.childCount; i++)
        {
            RectTransform card = content.GetChild(i) as RectTransform;
            if (card == null)
            {
                continue;
            }

            float signed = SignedDistanceToCenter(card);
            float distance = Mathf.Abs(signed);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestSigned = signed;
                nearest = card;
            }
        }

        if (nearest == null)
        {
            hasSnapTarget = false;
            return;
        }

        snapTargetX = content.anchoredPosition.x - nearestSigned;
        hasSnapTarget = true;
    }

    private float SignedDistanceToCenter(RectTransform card)
    {
        Vector3 cardLocal = viewport.InverseTransformPoint(card.position);
        return cardLocal.x - viewport.rect.center.x;
    }
}
