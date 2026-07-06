using System.Collections.Generic;
using UnityEngine;

public interface ILevelData
{
    string LevelDisplayName { get; }
    float timeLimit { get; }
    float scoreAddTime { get; }
    int waitingtime { get; }
    DifficultyLevel difficulty { get; }
    string storyIntroText { get; }
    string storyEndingText { get; }
    Sprite originalSprite { get; }
    Sprite distortedSprite { get; }
    AudioClip ambientSound { get; }
    AudioClip completionSound { get; }
    AudioClip gameoverSound { get; }
    List<DifferenceInfo> differences { get; }
}
