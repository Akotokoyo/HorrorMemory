using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private GameData _gameData;
    [SerializeField] private TextAsset[] translatedTXT;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    public GameState gameState = GameState.WAITING;
    [SerializeField] private bool useCasualMode = true;
    [SerializeField] private LevelData currentLevel;
    [SerializeField] private List<LevelData> _gameLevelConfigurations;

    private SaveData _saveData;

    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        LanguageManager.Initialize(translatedTXT);

        _saveData = new();
        var savedData = _saveData.ReadSaveFile();
        if (savedData == null)
        {
            _gameData = SetupGameData();
        }
        else
        {
            _gameData = savedData;
        }

        UIManager.Instance.GenerateLevelPrefabs(_gameData);
    }

    private void OnEnable()
    {
        LevelManager.OnLevelEnded += HandleLevelEnded;
    }
    private void OnDisable()
    {
        LevelManager.OnLevelEnded -= HandleLevelEnded;
    }

    private GameData SetupGameData()
    {
        JsonConverter jsonConverter = new JsonConverter();
        return jsonConverter.ConvertStartingGameDataAsync();
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

        LevelManager.Instance.PrepareLevel();
    }

    public void StartLevel()
    {
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

    public void UpdateGameData(int levelId, int starRating, float currentTimer)
    {
        Level level = _gameData.Levels[levelId];
        if (level.StarRating < starRating)
        {
            level.StarRating = starRating;
        }
        if(level.BestTimer < currentTimer)
        {
            level.BestTimer = (int)currentTimer;
        }

        if(levelId + 1 < _gameData.Levels.Count)
        {
            _gameData.Levels[levelId + 1].IsAvailable = true;
        }
        //TODO: use one JsonConverter
        _saveData.WriteFile(new JsonConverter(), _gameData);
    }

    public Level GetLevelFromGameData(int index)
    {
        return _gameData.Levels[index];
    }
}
