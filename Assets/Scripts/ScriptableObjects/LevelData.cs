using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData_", menuName = "Scriptable Objects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public string levelName = "Untitled Level";
    public int levelNumber = 1;
    public DifficultyLevel difficulty = DifficultyLevel.Medium;
    
    [Min(0f)]
    public float timeLimit = 180f;

    public float wrongClickPenalty = 5f;
    public int availableHints = 3;

    public Sprite originalSprite;
    public Sprite distortedSprite;

    [Header("Differences")]
    public List<DifferenceInfo> differences = new List<DifferenceInfo>();

    [Header("Sounds")]
    public AudioClip ambientSound;
    public AudioClip backgroundMusic;
    public AudioClip completionSound;
    public AudioClip gameoverSound;


    public bool IsLevelValid()
    {
        if(originalSprite == null || distortedSprite == null)
        {
            Debug.LogError("Missing Original or Distorted Sprite");
            return false;
        }

        if (differences.Count == 0)
        {
            Debug.LogError("There aren't any difference between the images");
            return false;
        }

        if(originalSprite.texture.width  != distortedSprite.texture.width ||
           originalSprite.texture.height != distortedSprite.texture.height)
        {
            Debug.LogError("The originalSprite and distortedSprite have different resolution");
            return false;
        }

        return true;
    }

    public void GetStarThreshold(out float threeStarTime, out float twoStarTime)
    {
        switch (difficulty)
        {
            case DifficultyLevel.Easy:
                threeStarTime = timeLimit * 0.5f;
                twoStarTime = timeLimit * 0.75f;
                break;
            case DifficultyLevel.Medium:
                threeStarTime = timeLimit * 0.6f;
                twoStarTime = timeLimit * 0.8f;
                break;
            case DifficultyLevel.Hard:
                threeStarTime = timeLimit * 0.7f;
                twoStarTime = timeLimit * 0.85f;
                break;
            default:
                threeStarTime = timeLimit * 0.6f;
                twoStarTime = timeLimit * 0.8f;
                break;
        }
    }
}
