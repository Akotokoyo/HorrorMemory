using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonConverter
{
    public GameData ConvertStartingGameDataAsync()
    {
#if UNITY_EDITOR
        return DownloadSaveFile<GameData>("Assets/Resources/StartingGameData/StartingGameData.json");
#else
        TextAsset text = Resources.Load<TextAsset>("StartingGameData/StartingGameData");
        return JsonConvert.DeserializeObject<GameData>(text.text);
#endif
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
