using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private GameData _gameData;
    [SerializeField] private TextAsset[] translatedTXT;

    [SerializeField] private AudioManager audioManager;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private GameState gameState = GameState.MAIN_MENU;
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
        audioManager.Initialize();

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
        UIManager.OnAudioChange += SetupAudio;
    }
    private void OnDisable()
    {
        LevelManager.OnLevelEnded -= HandleLevelEnded;
        UIManager.OnAudioChange -= SetupAudio;
    }

    private GameData SetupGameData()
    {
        JsonConverter jsonConverter = new JsonConverter();
        return jsonConverter.ConvertStartingGameDataAsync();
    }

    private void HandleLevelEnded(bool levelSuccess)
    {
        SetState(levelSuccess ? GameState.COMPLETED : GameState.GAME_OVER);
    }

    private void SetupAudio(bool audioOn)
    {
        audioManager.SetupAudio(audioOn);
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
        SetState(GameState.PLAYING);
        LevelManager.Instance.InitLevel();
    }

    public void StartGame(int levelId, bool isCasualMode = false)
    {
        SetState(GameState.WAITING);
        useCasualMode = isCasualMode;
        if (!useCasualMode)
        {
            currentLevel = _gameLevelConfigurations[levelId];
        }
        StartCoroutine(InitializeGame());
    }

    public void UpdateGameData(int levelId, int starRating, float currentTimer)
    {
        Level level = GetLevelById(levelId);
        if (level == null)
        {
            Debug.LogError($"Level with Id {levelId} not found in save data.");
            return;
        }
        if (level.StarRating < starRating)
        {
            level.StarRating = starRating;
        }
        if (level.BestTimer < currentTimer)
        {
            level.BestTimer = (int)currentTimer;
        }
        Level nextLevel = GetLevelById(levelId + 1);
        if (nextLevel != null)
        {
            nextLevel.IsAvailable = true;
        }
        _saveData.WriteFile(new JsonConverter(), _gameData);
    }

    public Level GetLevelFromGameData(int index)
    {
        return _gameData.Levels[index];
    }

    public void SetPauseState(bool isPaused)
    {
        SetState(isPaused ? GameState.PAUSED: GameState.PLAYING);
        LevelManager.Instance.PauseGame(isPaused);
    }

    public void SetMainMenuState()
    {
        SetState(GameState.MAIN_MENU);
        LevelManager.Instance.StopLevel();
    }

    private void SetState(GameState state) {
        gameState = state;
    }

    public Level GetLevelById(int levelId)
    {
        if (_gameData?.Levels == null)
        {
            return null;
        }
        for (int i = 0; i < _gameData.Levels.Count; i++)
        {
            if (_gameData.Levels[i].Id == levelId)
            {
                return _gameData.Levels[i];
            }
        }
        return null;
    }
}
