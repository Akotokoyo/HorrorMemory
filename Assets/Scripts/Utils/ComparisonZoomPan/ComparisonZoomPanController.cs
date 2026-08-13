using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComparisonZoomPanController : MonoBehaviour
{
    public static ComparisonZoomPanController Instance { get; private set; }

    //English Notion: Pan: Image Is Moving, Drag: Finger is Moving, Pinch: Two Fingers Are Moving
    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 3f;
    [SerializeField] private float pinchZoomSpeed = 0.005f;
    [SerializeField] private float scrollZoomSpeed = 0.15f;
    [SerializeField] private float panDragThreshold = 10f;

    private RectTransform leftViewport;
    private RectTransform rightViewport;
    private RectTransform leftContent;
    private RectTransform rightContent;
    private Canvas canvas;

    private float currentZoom = 1f;
    private Vector2 panOffset;
    private float lastPinchDistance;
    private bool isPinching;
    private bool isPanning;
    private bool suppressNextClick;
    private float accumulatedDragDistance;
    private Vector2 lastMousePosition;

    public bool IsGesturing => isPinching || isPanning || suppressNextClick;

    public void Setup(RectTransform distortedImageRect, RectTransform modifiedImageRect)
    {
        if (distortedImageRect == null || modifiedImageRect == null)
        {
            return;
        }

        canvas = distortedImageRect.GetComponentInParent<Canvas>();

        if (leftViewport == null)
        {
            BuildDualViewports(distortedImageRect, modifiedImageRect);
            return;
        }

        AttachImageToContent(distortedImageRect, leftContent);
        AttachImageToContent(modifiedImageRect, rightContent);
    }

    public void ResetView()
    {
        if (leftContent == null || rightContent == null)
        {
            return;
        }

        currentZoom = minZoom;
        panOffset = Vector2.zero;
        ApplyTransform();
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
        if (leftViewport == null || rightViewport == null)
        {
            return;
        }

        HandleTouchInput();

        if (Input.touchCount == 0)
        {
            HandleMouseInput();
        }
    }

    private void BuildDualViewports(RectTransform distortedImageRect, RectTransform modifiedImageRect)
    {
        Transform parent = distortedImageRect.parent;
        int siblingIndex = distortedImageRect.GetSiblingIndex();

        leftViewport = CreateViewport(parent, "LeftComparisonViewport", siblingIndex, 0f, 0.5f);
        rightViewport = CreateViewport(parent, "RightComparisonViewport", siblingIndex + 1, 0.5f, 1f);

        leftContent = CreateContent(leftViewport);
        rightContent = CreateContent(rightViewport);

        AttachImageToContent(distortedImageRect, leftContent);
        AttachImageToContent(modifiedImageRect, rightContent);

        var leftInput = leftViewport.gameObject.AddComponent<ComparisonZoomPanInput>();
        var rightInput = rightViewport.gameObject.AddComponent<ComparisonZoomPanInput>();
        leftInput.Initialize(this);
        rightInput.Initialize(this);
    }

    private static RectTransform CreateViewport(Transform parent, string name, int siblingIndex, float anchorMinX, float anchorMaxX)
    {
        var viewportObject = new GameObject(name, typeof(RectTransform), typeof(RectMask2D), typeof(Image));
        var viewport = viewportObject.GetComponent<RectTransform>();
        viewport.SetParent(parent, false);
        viewport.SetSiblingIndex(siblingIndex);

        viewport.anchorMin = new Vector2(anchorMinX, 0f);
        viewport.anchorMax = new Vector2(anchorMaxX, 0.9f);
        viewport.offsetMin = Vector2.zero;
        viewport.offsetMax = Vector2.zero;
        viewport.pivot = new Vector2(0.5f, 0.5f);

        var viewportImage = viewportObject.GetComponent<Image>();
        viewportImage.color = new Color(1f, 1f, 1f, 0f);
        viewportImage.raycastTarget = true;

        return viewport;
    }

    private static RectTransform CreateContent(RectTransform viewport)
    {
        var contentObject = new GameObject("Content", typeof(RectTransform));
        var content = contentObject.GetComponent<RectTransform>();
        content.SetParent(viewport, false);
        content.anchorMin = Vector2.zero;
        content.anchorMax = Vector2.one;
        content.offsetMin = Vector2.zero;
        content.offsetMax = Vector2.zero;
        content.pivot = new Vector2(0.5f, 0.5f);
        content.localScale = Vector3.one;
        return content;
    }

    private static void AttachImageToContent(RectTransform imageRect, RectTransform content)
    {
        imageRect.SetParent(content, false);
        imageRect.anchorMin = Vector2.zero;
        imageRect.anchorMax = Vector2.one;
        imageRect.offsetMin = Vector2.zero;
        imageRect.offsetMax = Vector2.zero;
        imageRect.pivot = new Vector2(0.5f, 0.5f);
        imageRect.anchoredPosition = Vector2.zero;
    }

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

        RectTransform referenceViewport = GetViewportForScreenPoint(screenPoint);
        Vector2 normalizedPoint = GetNormalizedInViewport(referenceViewport, screenPoint);
        Vector2 focalPoint = NormalizedToLocalPoint(referenceViewport, normalizedPoint);

        float previousZoom = currentZoom;
        currentZoom = Mathf.Clamp(currentZoom + zoomDelta, minZoom, maxZoom);

        if (Mathf.Approximately(previousZoom, currentZoom))
        {
            return;
        }

        float scaleFactor = currentZoom / previousZoom;
        panOffset = focalPoint - (focalPoint - panOffset) * scaleFactor;

        if (currentZoom <= minZoom + 0.001f)
        {
            panOffset = Vector2.zero;
        }

        ClampPanOffset();
        ApplyTransform();
    }

    private void Pan(Vector2 delta)
    {
        panOffset += delta;
        ClampPanOffset();
        ApplyTransform();
    }

    private void ApplyTransform()
    {
        Vector3 scale = Vector3.one * currentZoom;
        leftContent.localScale = scale;
        rightContent.localScale = scale;
        leftContent.anchoredPosition = panOffset;
        rightContent.anchoredPosition = panOffset;
    }

    private void ClampPanOffset()
    {
        Vector2 viewportSize = leftViewport.rect.size;
        Vector2 scaledSize = leftContent.rect.size * currentZoom;

        float maxOffsetX = Mathf.Max(0f, (scaledSize.x - viewportSize.x) * 0.5f);
        float maxOffsetY = Mathf.Max(0f, (scaledSize.y - viewportSize.y) * 0.5f);

        panOffset.x = Mathf.Clamp(panOffset.x, -maxOffsetX, maxOffsetX);
        panOffset.y = Mathf.Clamp(panOffset.y, -maxOffsetY, maxOffsetY);
    }

    private RectTransform GetViewportForScreenPoint(Vector2 screenPoint)
    {
        Camera eventCamera = GetEventCamera();

        if (RectTransformUtility.RectangleContainsScreenPoint(leftViewport, screenPoint, eventCamera))
        {
            return leftViewport;
        }

        return rightViewport;
    }

    private Vector2 GetNormalizedInViewport(RectTransform viewport, Vector2 screenPoint)
    {
        Camera eventCamera = GetEventCamera();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(viewport, screenPoint, eventCamera, out Vector2 localPoint);

        Rect rect = viewport.rect;
        return new Vector2(localPoint.x / rect.width + 0.5f, localPoint.y / rect.height + 0.5f);
    }

    private static Vector2 NormalizedToLocalPoint(RectTransform viewport, Vector2 normalizedPoint)
    {
        Rect rect = viewport.rect;
        return new Vector2(
            (normalizedPoint.x - 0.5f) * rect.width,
            (normalizedPoint.y - 0.5f) * rect.height);
    }

    private Camera GetEventCamera()
    {
        return canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay
            ? canvas.worldCamera
            : null;
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
