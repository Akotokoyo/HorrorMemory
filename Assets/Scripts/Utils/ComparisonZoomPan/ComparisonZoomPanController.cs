using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComparisonZoomPanController : MonoBehaviour
{
    public static ComparisonZoomPanController Instance { get; private set; }

    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 3f;
    [SerializeField] private float pinchZoomSpeed = 0.005f;
    [SerializeField] private float scrollZoomSpeed = 0.15f;
    [SerializeField] private float panDragThreshold = 10f;

    private RectTransform viewport;
    private RectTransform content;
    private Canvas canvas;

    private float currentZoom = 1f;
    private float lastPinchDistance;
    private bool isPinching;
    private bool isPanning;
    private bool suppressNextClick;
    private float accumulatedDragDistance;

    public bool IsGesturing => isPinching || isPanning || suppressNextClick;

    public void Setup(RectTransform distortedImageRect, RectTransform modifiedImageRect)
    {
        if (viewport != null)
        {
            return;
        }

        canvas = GetComponentInParent<Canvas>();
        BuildViewport(distortedImageRect, modifiedImageRect);
    }

    public void ResetView()
    {
        if (content == null)
        {
            return;
        }

        currentZoom = minZoom;
        content.localScale = Vector3.one;
        content.anchoredPosition = Vector2.zero;
        isPinching = false;
        isPanning = false;
        suppressNextClick = false;
        accumulatedDragDistance = 0f;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Update()
    {
        if (viewport == null)
        {
            return;
        }

        HandleTouchInput();

        if (Input.touchCount == 0)
        {
            HandleMouseInput();
        }
    }

    private void BuildViewport(RectTransform distortedImageRect, RectTransform modifiedImageRect)
    {
        Transform parent = distortedImageRect.parent;

        var viewportObject = new GameObject("ComparisonViewport", typeof(RectTransform), typeof(RectMask2D), typeof(Image));
        viewport = viewportObject.GetComponent<RectTransform>();
        viewport.SetParent(parent, false);
        viewport.SetSiblingIndex(distortedImageRect.GetSiblingIndex());

        viewport.anchorMin = new Vector2(0f, 0f);
        viewport.anchorMax = new Vector2(1f, 0.9f);
        viewport.offsetMin = Vector2.zero;
        viewport.offsetMax = Vector2.zero;
        viewport.pivot = new Vector2(0.5f, 0.5f);

        var viewportImage = viewportObject.GetComponent<Image>();
        viewportImage.color = new Color(1f, 1f, 1f, 0f);
        viewportImage.raycastTarget = true;

        var contentObject = new GameObject("ComparisonContent", typeof(RectTransform));
        content = contentObject.GetComponent<RectTransform>();
        content.SetParent(viewport, false);
        content.anchorMin = Vector2.zero;
        content.anchorMax = Vector2.one;
        content.offsetMin = Vector2.zero;
        content.offsetMax = Vector2.zero;
        content.pivot = new Vector2(0.5f, 0.5f);
        content.localScale = Vector3.one;

        ReparentImagePanel(distortedImageRect, new Vector2(0f, 0f), new Vector2(0.5f, 1f));
        ReparentImagePanel(modifiedImageRect, new Vector2(0.5f, 0f), new Vector2(1f, 1f));

        var inputHandler = viewportObject.AddComponent<ComparisonZoomPanInput>();
        inputHandler.Initialize(this);
    }

    private void ReparentImagePanel(RectTransform imageRect, Vector2 anchorMin, Vector2 anchorMax)
    {
        imageRect.SetParent(content, false);
        imageRect.anchorMin = anchorMin;
        imageRect.anchorMax = anchorMax;
        imageRect.offsetMin = Vector2.zero;
        imageRect.offsetMax = Vector2.zero;
        imageRect.pivot = new Vector2(0.5f, 0.5f);
        imageRect.anchoredPosition = Vector2.zero;
    }

    private Vector2 lastMousePosition;

    private void HandleMouseInput()
    {
        float scrollDelta = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scrollDelta) > 0.01f)
        {
            ApplyZoomAtScreenPoint(Input.mousePosition, scrollDelta * scrollZoomSpeed);
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            accumulatedDragDistance = 0f;
            isPanning = false;
            suppressNextClick = false;
        }

        if (Input.GetMouseButton(0) && currentZoom > minZoom + 0.001f)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 delta = currentMousePosition - lastMousePosition;
            lastMousePosition = currentMousePosition;

            if (delta.sqrMagnitude <= 0f)
            {
                return;
            }

            accumulatedDragDistance += delta.magnitude;

            if (accumulatedDragDistance >= panDragThreshold)
            {
                isPanning = true;
                suppressNextClick = true;
                Pan(delta);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPanning = false;
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 2)
        {
            Touch touchA = Input.GetTouch(0);
            Touch touchB = Input.GetTouch(1);
            float distance = Vector2.Distance(touchA.position, touchB.position);
            Vector2 pinchCenter = (touchA.position + touchB.position) * 0.5f;

            if (!isPinching)
            {
                isPinching = true;
                isPanning = false;
                lastPinchDistance = distance;
                suppressNextClick = false;
                return;
            }

            float pinchDelta = distance - lastPinchDistance;
            ApplyZoomAtScreenPoint(pinchCenter, pinchDelta * pinchZoomSpeed);
            lastPinchDistance = distance;

            if (Mathf.Abs(pinchDelta) > 0.01f)
            {
                suppressNextClick = true;
            }

            return;
        }

        if (isPinching && Input.touchCount < 2)
        {
            isPinching = false;
            lastPinchDistance = 0f;
        }

        if (Input.touchCount == 1 && currentZoom > minZoom + 0.001f)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isPanning = false;
                accumulatedDragDistance = 0f;
                suppressNextClick = false;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                accumulatedDragDistance += touch.deltaPosition.magnitude;

                if (accumulatedDragDistance >= panDragThreshold)
                {
                    isPanning = true;
                    suppressNextClick = true;
                }

                if (isPanning)
                {
                    Pan(touch.deltaPosition);
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isPanning = false;
            }
        }
        else if (Input.touchCount == 0 && !isPinching)
        {
            isPanning = false;
        }
    }

    internal void BeginPointerDrag(Vector2 pointerPosition)
    {
        if (Input.touchCount > 0 || currentZoom <= minZoom + 0.001f)
        {
            return;
        }

        accumulatedDragDistance = 0f;
        isPanning = false;
        suppressNextClick = false;
    }

    internal void PointerDrag(Vector2 delta)
    {
        if (Input.touchCount > 0 || currentZoom <= minZoom + 0.001f)
        {
            return;
        }

        accumulatedDragDistance += delta.magnitude;

        if (accumulatedDragDistance >= panDragThreshold)
        {
            isPanning = true;
            suppressNextClick = true;
            Pan(delta);
        }
    }

    internal void EndPointerDrag()
    {
        isPanning = false;
    }

    internal void PointerScroll(Vector2 screenPoint, float scrollDelta)
    {
        ApplyZoomAtScreenPoint(screenPoint, scrollDelta * scrollZoomSpeed);
    }

    private void ApplyZoomAtScreenPoint(Vector2 screenPoint, float zoomDelta)
    {
        if (Mathf.Approximately(zoomDelta, 0f))
        {
            return;
        }

        Camera eventCamera = canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay
            ? canvas.worldCamera
            : null;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(viewport, screenPoint, eventCamera, out Vector2 localPoint))
        {
            return;
        }

        float previousZoom = currentZoom;
        currentZoom = Mathf.Clamp(currentZoom + zoomDelta, minZoom, maxZoom);

        if (Mathf.Approximately(previousZoom, currentZoom))
        {
            return;
        }

        float scaleFactor = currentZoom / previousZoom;
        Vector2 contentPosition = content.anchoredPosition;
        contentPosition = localPoint - (localPoint - contentPosition) * scaleFactor;

        content.localScale = Vector3.one * currentZoom;
        content.anchoredPosition = contentPosition;

        if (currentZoom <= minZoom + 0.001f)
        {
            content.anchoredPosition = Vector2.zero;
        }

        ClampContentPosition();
    }

    private void Pan(Vector2 delta)
    {
        content.anchoredPosition += delta / currentZoom;
        ClampContentPosition();
    }

    private void ClampContentPosition()
    {
        Vector2 scaledSize = content.rect.size * currentZoom;
        Vector2 viewportSize = viewport.rect.size;

        float maxOffsetX = Mathf.Max(0f, (scaledSize.x - viewportSize.x) * 0.5f);
        float maxOffsetY = Mathf.Max(0f, (scaledSize.y - viewportSize.y) * 0.5f);

        Vector2 clampedPosition = content.anchoredPosition;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -maxOffsetX, maxOffsetX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -maxOffsetY, maxOffsetY);
        content.anchoredPosition = clampedPosition;
    }
}

internal class ComparisonZoomPanInput : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
{
    private ComparisonZoomPanController controller;

    public void Initialize(ComparisonZoomPanController zoomPanController)
    {
        controller = zoomPanController;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 0)
        {
            return;
        }

        controller?.BeginPointerDrag(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 0)
        {
            return;
        }

        controller?.PointerDrag(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        controller?.EndPointerDrag();
    }

    public void OnScroll(PointerEventData eventData)
    {
        controller?.PointerScroll(eventData.position, eventData.scrollDelta.y);
    }
}
