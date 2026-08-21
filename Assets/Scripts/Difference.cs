using UnityEngine;
using UnityEngine.EventSystems;

public class Difference : MonoBehaviour, IPointerClickHandler
{
    public DifferenceInfo diffInfo;
    public int diffIndex;
    public bool isFound = false;
    public bool isClickable = true;

    public void OnPointerClick(PointerEventData ev)
    {
        if (ComparisonZoomPanController.Instance != null && ComparisonZoomPanController.Instance.IsGesturing)
        {
            return;
        }

        if (!isFound && isClickable)
        {
            LevelManager.Instance.OnDifferenceClicked(diffIndex);
        }
    }
}
