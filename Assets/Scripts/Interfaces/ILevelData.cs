using System.Collections.Generic;
using UnityEngine;

public interface ILevelData
{
    int LevelId { get; }
    string LevelDisplayName { get; }
    float timeLimit { get; }
    DifficultyLevel difficulty { get; }
    string storyIntroText { get; }
    string storyEndingText { get; }
    Sprite originalSprite { get; }
    Sprite modifiedSprite { get; }
    Sprite distortedSprite { get; }
    AudioClip completionSound { get; }
    AudioClip wrongSound { get; }
    List<DifferenceInfo> differences { get; }
}
