using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public class JsonConverter
{
    public GameData ConvertStartingGameDataAsync()
    {
        return DownloadSaveFile<GameData>("Assets/Resources_moved/StartingGameData/StartingGameData.json");
    }

    public string WriteToJsonFromObject<T>(T GeneralObject)
    {
        return JsonConvert.SerializeObject(GeneralObject);
    }

    #region Convert Functions
    private void WriteToJsonFromList<T>(string jsonFilePath, List<T> PropertyList)
    {
        string jsonFromObjectList = JsonConvert.SerializeObject(PropertyList);
        File.WriteAllText(jsonFilePath, jsonFromObjectList);
    }

    private T DownloadSaveFile<T>(string jsonFilePath)
    {
        string jsonText = File.ReadAllText(jsonFilePath);
        return JsonConvert.DeserializeObject<T>(jsonText);
    }

    #endregion
}
