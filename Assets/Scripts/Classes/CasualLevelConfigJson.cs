using System;
using System.Collections.Generic;

[Serializable]
public class CasualLevelConfigJson
{
    public List<CasualDifficultyJson> difficulties;

    public List<LevelConfig> levels;
}

[Serializable]
public class CasualDifficultyJson
{
    public DifficultyLevel level;
    public int differencesToFind;
    public float timeLimit;
    public float pickWeight = 1f;
}

[Serializable]
public class LevelConfig
{
    public int id;
    public bool enabled;
    public List<CasualLevelDifferenceSlots> differenceSlots;
}

[Serializable]
public class CasualLevelDifferenceSlots
{
    public int slotIndex;
    public List<string> sprites;
}
