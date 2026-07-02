using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class transaleteLanguage : MonoBehaviour
{
   private string messageId = string.Empty;
   private string customValue = string.Empty;
    private List<string> variableValues= new();

   private void Start()
    {
        if (string.IsNullOrEmpty(messageId))
        {
            messageId = this.GetComponent<TextMeshProUGUI>().text;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        if (languageManager.Instance == null) return;

        if(this.GetComponent<TextMeshProUGUI>() != null)
        {
            // this.GetComponent<TextMeshProUGUI>().text = languageManager.Instance.TranslateText(messageId)+customValue;
            this.GetComponent<TextMeshProUGUI>().text = $"{languageManager.Instance.TranslateText(messageId)}  {customValue}";
        }
    }

    public void UpdateTextConcat(string message, string concat)
    {
        messageId = message;
        customValue = concat;

        UpdateText();
    }
                                                                               
    public void UpdateReplaceVariables(string message, List<string> variableCustom)
    {
        this.messageId = message;
        string[] splittedStrinfs = message.Split("%%");
        string finalString = string.Empty;

        if(variableCustom.Count +1 == splittedStrinfs.Length ||
            variableCustom.Count  == splittedStrinfs.Length)
        {
           for(int i = 0; i < splittedStrinfs.Length; i++)
            {
                finalString += $"{splittedStrinfs[i]} {customValue[i]}";
            }

            finalString += variableCustom.Count + 1 == splittedStrinfs.Length ? splittedStrinfs[splittedStrinfs.Length] : "";
           this.GetComponent<TextMeshProUGUI>().text = finalString;
        }
        else
        {
            Debug.LogWarning("manca valore");
        }
    }
}
