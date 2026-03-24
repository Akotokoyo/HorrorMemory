using UnityEngine;

public class introMenu : MonoBehaviour
{

    public GameObject levelSelection;
    public GameObject menu;

    public void SceltaMenu(int action)
    {
        switch (action)
        {
            case 0:
                OnClickProva();
                break;
            case 1:
                OnClickOpzioni();
                break;
            case 2:
                OnClickUscire();
                break;
            default:
                Debug.LogWarning("nessuna azione");
                break;
        }
    }


    private void OnClickProva()
    {
        //attivare o disattivare un oggetto
        levelSelection.SetActive(true);
        menu.SetActive(false);
    }

    private void OnClickOpzioni()
    {
        Debug.Log("sonop stato cliccato per opzionare");
    }

    private void OnClickUscire()
    {
        Debug.Log("sonop stato cliccato per buttarti fuori >:(");
    }

    public void LevelSelectionActions()
    {
        levelSelection.SetActive(false);
        menu.SetActive(true);                                 
    }
}
