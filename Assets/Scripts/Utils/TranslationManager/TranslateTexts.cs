using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TranslateTexts : MonoBehaviour
{
    private string idMessage;
    private string concatText = "";
    private List<string> variablesCached;
    private bool hasStarted;

    private void Start()
    {
        if (string.IsNullOrEmpty(idMessage))
        {
            idMessage = this.GetComponent<TextMeshProUGUI>().text;
        }
        hasStarted = true;
        UpdateText();
    }

    private void OnEnable()
    {
        LanguageManager.OnLanguageChangedEvent += UpdateText;
        if (!hasStarted) return;
        if (string.IsNullOrEmpty(idMessage) && this.GetComponent<TextMeshProUGUI>() != null)
        {
            idMessage = this.GetComponent<TextMeshProUGUI>().text;
        }
        UpdateText();
    }

    private void OnDisable()
    {
        LanguageManager.OnLanguageChangedEvent -= UpdateText;
    }

    private void UpdateText()
    {

        if (LanguageManager.Instance == null) return;

        TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
        if(textComponent == null)
        {
            Debug.LogWarning("[TranslateTexts] missing TextMeshProUGUI.");
            return;
        }

        string translatedText = LanguageManager.Instance.TranslateText(idMessage);
        textComponent.text = ReplaceVariables(translatedText) + concatText;
    }

    public void ChangeTextByScript(string idMessage, string concat = "")
    {
        this.idMessage = idMessage;
        this.concatText = concat;
        variablesCached = null;
        UpdateText();
    }

    public void ChangeTextAndReplaceVariables(string idMessage, List<string> variables)
    {
        this.idMessage = idMessage;
        concatText = "";
        variablesCached = variables != null
            ? new List<string>(variables)
            : null;
        UpdateText();
    }

    private string ReplaceVariables(string translatedText)
    {
        if (variablesCached == null || variablesCached.Count == 0)
        {
            return translatedText;
        }
        string[] parts = translatedText.Split("%%");
        if (parts.Length != variablesCached.Count + 1)
        {
            Debug.LogWarning($"[TranslateTexts] Placeholder non validi per '{idMessage}'.");
            return translatedText;
        }
        string result = parts[0];
        for (int i = 0; i < variablesCached.Count; i++)
        {
            result += variablesCached[i] + parts[i + 1];
        }
        return result;
    }
}