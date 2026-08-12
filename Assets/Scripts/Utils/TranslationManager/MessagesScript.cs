using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Messages
{
    Dictionary<string, string> dict;

    public Messages(TextAsset langTxtFile)
    {
        JsonToDictionary(langTxtFile.text);
    }

    private void JsonToDictionary(string jsonString)
    {
        try
        {
            dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        }
        catch (JsonException e)
        {
            Debug.LogError($"Errore durante il parsing del JSON: {e.Message}");
        }
    }
    public string GetMessage(string msgId)
    {
        return dict.ContainsKey(msgId) && dict[msgId].Length > 0 ? dict[msgId] : msgId;
    }
}