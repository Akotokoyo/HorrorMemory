using UnityEngine;

[System.Serializable]
public class DifferenceInfo
{
    public Vector2 normalizedPosition;

    [Range(10f, 100f)]
    public float deflectinRadious = 50f;

    public int width = 200;
    public int height = 200;

    public Sprite startedSprite;
    public Sprite distortedSprite;
    public bool mustBeFound;
}
