using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Difference : MonoBehaviour, IPointerClickHandler
{
    public DifferenceInfo diffInfo;
    public int diffIndex;
    public bool isFound = false;
    public bool isClickable = true;

    public delegate void DifferenceFoundEvent(Difference diff);
    public event DifferenceFoundEvent OnDifferenceFound;

    public void OnPointerClick(PointerEventData ev)
    {
        Debug.Log($"Indexed Object Clicked {diffIndex}");
        if (!isFound && isClickable)
        {
            RevealDifference();
            LevelManager.Instance.OnDifferenceClicked(diffIndex);
        }
    }

    private void RevealDifference()
    {
        this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        isFound = true;
    }
}
