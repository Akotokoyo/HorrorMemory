
using System.Collections.Generic;

public class GameData
{
    public List<Level> Levels;

    public GameData(List<Level> levels)
    {
        Levels = levels;
    }
}

public class Level
{
    public int Id;
    public string LevelName;
    public string AddrImage;
    public int StarRating;
    public string BestTime;
}