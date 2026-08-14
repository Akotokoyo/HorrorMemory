using System.Collections.Generic;
using UnityEngine;

public class RuntimeLevelData : ILevelData
{
    public string levelName = "Untitled Level";
    public int levelId = 1;
    public DifficultyLevel difficulty = DifficultyLevel.Medium;
    public float timeLimit = 20f;
    public float scoreAddTime = 5f;
    public int waitingtime = 5;
    public string storyIntroText = string.Empty;
    public string storyEndingText = string.Empty;

    public Sprite originalSprite;
    public Sprite distortedSprite;
    public AudioClip ambientSound;
    public AudioClip completionSound;
    public AudioClip gameoverSound;

    public List<DifferenceInfo> differences = new List<DifferenceInfo>();

    public string LevelDisplayName => levelName;
    int ILevelData.LevelId => levelId;
    float ILevelData.timeLimit => timeLimit;
    DifficultyLevel ILevelData.difficulty => difficulty;
    string ILevelData.storyIntroText => storyIntroText;
    string ILevelData.storyEndingText => storyEndingText;
    Sprite ILevelData.originalSprite => originalSprite;
    Sprite ILevelData.distortedSprite => distortedSprite;
    AudioClip ILevelData.completionSound => completionSound;
    List<DifferenceInfo> ILevelData.differences => differences;
}
