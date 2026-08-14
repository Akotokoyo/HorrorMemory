using System;

public class GeneralFunctions
{
    public static bool IsBestTime(string savedTime, string newTime)
    {
        if(savedTime == "")
        {
            return true;
        }

        return false;
    }

    public static int CalculateStarRating(ILevelData levelData, float currentTimer)
    {
        if ((currentTimer < levelData.timeLimit && currentTimer >= levelData.timeLimit * Constants.FIRST_STAR_RANGE_PERCENTAGE) || currentTimer >= levelData.timeLimit) return 3;
        if (currentTimer < levelData.timeLimit * Constants.FIRST_STAR_RANGE_PERCENTAGE && currentTimer >= levelData.timeLimit * Constants.SECOND_STAR_RANGE_PERCENTAGE) return 2;
        if (currentTimer < levelData.timeLimit * Constants.SECOND_STAR_RANGE_PERCENTAGE && currentTimer > 0) return 1;
        return 0;
    }
}