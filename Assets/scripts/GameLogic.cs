using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameLogic : MonoBehaviour
{

    [SerializeField] List<GameObject> buttons;
    [SerializeField] TextMeshProUGUI diffText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject winScreeen;
    [SerializeField] GameObject loseScreeen;
    [SerializeField] List<GameObject> buttonsEvents;


    [SerializeField] int nDiff = 0;

    int timer = 5;

    void Start()
    {
        diffText.GetComponent<transaleteLanguage>().UpdateTextConcat("infoBar_0", $"{nDiff}/{buttons.Count}");
        StartCoroutine(UptateTimer());
    }

    public void DiffFound()
    {
        nDiff += 1;

    }

    public void FoundButton(int index)
    {
        buttons[index].SetActive(false);
        DiffFound();
        diffText.GetComponent<transaleteLanguage>().UpdateTextConcat("infoBar_0", $"{nDiff}/{buttons.Count}");

        if (nDiff == buttons.Count) {
            WinScreen();
        }


    }

    public IEnumerator UptateTimer()
    {
        while (true)
        {
            timerText.GetComponent<transaleteLanguage>().UpdateTextConcat("infoBar_1", $"{timer}");
            yield return new WaitForSeconds(1);
            timer -= 1;
            if (timer == -1)
            {
                LoseScreen();
                break;
            }
        }
    }

    public void WinScreen()
    {
        Debug.Log("hai vinto");
        winScreeen.SetActive(true);
        for(int i = 0; i < buttonsEvents.Count; i++)
        {
            buttonsEvents[i].SetActive(true);
        }
    }

    public void LoseScreen()
    {
        Debug.Log("hai perso");
        loseScreeen.SetActive(true);
        for (int i = 0; i < buttonsEvents.Count; i++)
        {
            buttonsEvents[i].SetActive(true);
        }
    }

}
