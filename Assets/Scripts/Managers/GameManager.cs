using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] private GameState gameState = GameState.WAITING;
    [SerializeField] private LevelData currentLevel;


    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        if(gameState == GameState.WAITING)
        {
            StartCoroutine(LevelManager.Instance.PrepareLevel());
            gameState = GameState.PLAYING;
            LevelManager.Instance.InitLevel();
        }
    }
}
