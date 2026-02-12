using Unity.VisualScripting;
using UnityEngine;

public class NormalizedPositionHelper : MonoBehaviour
{
    [SerializeField] private RectTransform parentImage;
    private RectTransform myRect;

    private void Start()
    {
        myRect = this.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintNormalizedPosition();
        }
    }

    private void PrintNormalizedPosition()
    {
        if (parentImage == null) {
            Debug.LogError("Parent Image is not setted");
            return;
        }

        float width = parentImage.rect.width;
        float height = parentImage.rect.height;

        float normalizedX = (myRect.anchoredPosition.x / width) + 0.5f;
        float normalizedY = (myRect.anchoredPosition.y / height) + 0.5f;

        Debug.Log($"normalizedPosition: ({normalizedX:F3}, {normalizedY:F3})");
        Debug.Log($"Copia questo: new Vector2({normalizedX}f, {normalizedY}f)");
    }

}
