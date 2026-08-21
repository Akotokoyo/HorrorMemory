using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager
{
    public static LanguageManager Instance { get; private set; }

    public int languageId;
    private Dictionary<int, Dictionary<string, string>> languageCache = new Dictionary<int, Dictionary<string, string>>();

    public static System.Action OnLanguageChangedEvent;

    public LanguageManager(TextAsset[] translatedTXT)
    {
        if (!PlayerPrefs.HasKey("LanguageId"))
        {
            PlayerPrefs.SetInt("LanguageId", 0);
        }
        languageId = PlayerPrefs.GetInt("LanguageId", 0);

        PreloadAllLanguages(translatedTXT);
    }

    public static void Initialize(TextAsset[] translatedTXT)
    {
        if (Instance == null)
        {
            Instance = new LanguageManager(translatedTXT);
        }
    }


    private void PreloadAllLanguages(TextAsset[] translatedTXT)
    {
        for (int i = 0; i < translatedTXT.Length; i++)
        {
            if (translatedTXT[i] != null)
            {
                languageCache[i] = JsonConvert.DeserializeObject<Dictionary<string, string>>(translatedTXT[i].text);
            }
        }
    }

    public void OnLanguageChanged()
    {
        int newLanguageId = PlayerPrefs.GetInt("LanguageId", 0);
        if (newLanguageId != languageId)
        {
            languageId = newLanguageId;
            OnLanguageChangedEvent?.Invoke();
        }
    }

    public string TranslateText(string idMessage)
    {
        if (languageCache.TryGetValue(languageId, out Dictionary<string, string> currentLanguageDict))
        {
            return currentLanguageDict.TryGetValue(idMessage, out string translatedText) && !string.IsNullOrEmpty(translatedText)
                ? translatedText
                : idMessage;
        }
        return idMessage;
    }

    public void ChangeLanguage(int newLanguageId)
    {
        languageId = newLanguageId;
        PlayerPrefs.SetInt("LanguageId", languageId);
        OnLanguageChangedEvent?.Invoke();
    }
}