using UnityEngine;
using UnityEngine.EventSystems;

public class Difference : MonoBehaviour, IPointerClickHandler
{
    public DifferenceInfo diffInfo;
    public int diffIndex;
    public bool isFound = false;
    public bool isClickable = true;

    public delegate void DifferenceFoundEvent(Difference diff);

    public void OnPointerClick(PointerEventData ev)
    {
        Debug.Log($"Indexed Object Clicked {diffIndex}");
        if (!isFound && isClickable)
        {
            LevelManager.Instance.OnDifferenceClicked(diffIndex);
        }
    }
}
