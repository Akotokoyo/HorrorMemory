using System;

[Serializable]
public class CasualLevelConfigJson
{
    public string levelName = "Casual Level";
    public int minDifferencesToFind = 5;
    public int maxDifferencesToFind = 15;
    public float timeLimit = 20f;
    public float scoreAddTime = 5f;
    public int waitingtime = 5;
    public int difficulty = 1;

    public string originalImageAddress;
    public string distortedImageAddress;

    public string ambientSoundAddress;
    public string completionSoundAddress;

    public CasualDifferenceSlotJson[] differenceSlots;
}

[Serializable]
public class CasualDifferenceSlotJson
{
    public float posX;
    public float posY;
    public int width = 200;
    public int height = 200;
    public float deflectinRadious = 50f;

    public string[] startedSpriteAddresses;
    public string[] distortedSpriteAddresses;
}
