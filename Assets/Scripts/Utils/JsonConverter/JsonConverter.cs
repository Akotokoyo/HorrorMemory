using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class JsonConverter
{
    public IEnumerator ConvertStartingLevelsAsync(System.Action<GameData> onLoaded)
    {
        return ConvertJsonToObjectAsync($"StartingGameData/StartingGameData", onLoaded);
    }

    #region Convert Functions
    private IEnumerator ConvertJsonToObjectAsync<T>(string jsonFilePath, System.Action<T> onLoaded)
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>(jsonFilePath);
        yield return handle;

        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            T result = JsonConvert.DeserializeObject<T>(handle.Result.text);
            Addressables.Release(handle);
            onLoaded?.Invoke(result);
        }
        else
        {
            Debug.LogError($"Failed to load: {jsonFilePath}");
            onLoaded?.Invoke(default);
        }
    }

    private IEnumerator ConvertJsonToListOfObjectsAsync<T>(string jsonFilePath, System.Action<List<T>> onLoaded)
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>(jsonFilePath);
        yield return handle;

        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            List<T> result = JsonConvert.DeserializeObject<List<T>>(handle.Result.text);
            Addressables.Release(handle);
            onLoaded?.Invoke(result);
        }
        else
        {
            Debug.LogError($"Failed to load: {jsonFilePath}");
            onLoaded?.Invoke(null);
        }
    }

    public IEnumerator LoadJsonListAsync<T>(string jsonFilePath, System.Action<List<T>> onLoaded)
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>(jsonFilePath);
        yield return handle;

        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            List<T> result = JsonConvert.DeserializeObject<List<T>>(handle.Result.text);
            Addressables.Release(handle);
            onLoaded?.Invoke(result);
        }
        else
        {
            Debug.LogError($"Failed to load: {jsonFilePath}");
            onLoaded?.Invoke(null);
        }
    }

    private void WriteToJsonFromList<T>(string jsonFilePath, List<T> PropertyList)
    {
        string jsonFromObjectList = JsonConvert.SerializeObject(PropertyList);
        System.IO.File.WriteAllText(jsonFilePath, jsonFromObjectList);
    }

    public string WriteToJsonFromObject<T>(T GeneralObject)
    {
        return JsonConvert.SerializeObject(GeneralObject);
    }
    #endregion
}
