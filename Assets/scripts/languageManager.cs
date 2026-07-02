using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class languageManager
{
    public static languageManager Instance { get; private set; }

    private static Dictionary<int, Dictionary<string, string>> languagesCache = new Dictionary<int, Dictionary<string, string>>();

    private static int languageIdCached;

    public static void Initialize(TextAsset[] translateTXT)
    {
        if(Instance == null)
        {
            /*if(PlayerPrefs.GetInt("LanguageId", -1) == -1)
            {
                throw new Exception
            } */

            if (!PlayerPrefs.HasKey("Language"))
            {
                PlayerPrefs.SetInt("LanguageId", 0);
            }
            
            Debug.Log("sss");
            Instance = new languageManager();
            PreLoadLanguages(translateTXT);
            languageIdCached = PlayerPrefs.GetInt("LanguageId", 0);
        }
    }

    public string TranslateText(string languageCache)
    {
        return languagesCache[languageIdCached].GetValueOrDefault(languageCache, "Traslation not found");
    }

    private static void PreLoadLanguages(TextAsset[] translateJson)
    {
        for(int i = 0; i< translateJson.Length; i++)
        {
            languagesCache[i] = JsonConvert.DeserializeObject<Dictionary<string, string>>(translateJson[i].text);
        }
    }
}
