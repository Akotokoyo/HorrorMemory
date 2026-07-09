using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private GameData _gameData;
    
    public static GameManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] private GameState gameState = GameState.WAITING;
    [SerializeField] private bool useCasualMode = true;
    [SerializeField] private LevelData currentLevel;
    [SerializeField] private List<LevelData> _gameLevelConfigurations;


    private bool isNewGame = true;

    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        //TODO: Handle if save file exists
        if (isNewGame)
        {
            StartCoroutine(SetupGameData());
        }
    }

    private void OnEnable()
    {
        LevelManager.OnLevelEnded += HandleLevelEnded;
    }
    private void OnDisable()
    {
        LevelManager.OnLevelEnded -= HandleLevelEnded;
    }

    private IEnumerator SetupGameData()
    {
        JsonConverter jsonConverter = new JsonConverter();
        yield return jsonConverter.ConvertStartingLevelsAsync(startingGameData => _gameData = startingGameData);
    }

    private void HandleLevelEnded(bool levelSuccess)
    {
        gameState = levelSuccess ? GameState.COMPLETED : GameState.GAME_OVER;
    }

    private IEnumerator InitializeGame()
    {
        if (useCasualMode && CasualLevelGenerator.Instance != null)
        {
            LevelManager.Instance.currentLevel = null;
            bool done = false;
            yield return CasualLevelGenerator.Instance.GenerateLevelAsync(
                levelData =>
                {
                    LevelManager.Instance.currentLevel = levelData;
                    done = true;
                },
                error =>
                {
                    Debug.LogError(error);
                    done = true;
                });
            if (!done || LevelManager.Instance.currentLevel == null)
                yield break;
        }
        else if (!useCasualMode && currentLevel != null)
        {
            LevelManager.Instance.currentLevel = currentLevel;
        }
        else
        {
            Debug.LogError("No Level configured. Set CurrentLevel Or Active useCasualMode.");
            yield break;
        }

        yield return StartCoroutine(LevelManager.Instance.PrepareLevel());
        gameState = GameState.PLAYING;
        LevelManager.Instance.InitLevel();
    }

    public void StartGame(int levelId, bool isCasualMode = false)
    {
        gameState = GameState.WAITING;
        useCasualMode = isCasualMode;
        if (!useCasualMode)
        {
            currentLevel = _gameLevelConfigurations[levelId];
        }
        if (gameState == GameState.WAITING)
        {
            StartCoroutine(InitializeGame());
        }
    }
}
