using UnityEngine;

public class gameManager : MonoBehaviour
{
    [SerializeField] TextAsset[] textAssets;

    private void Awake()
    {
        if (languageManager.Instance == null)
        {
            languageManager.Initialize(textAssets);
        }

    }
}
