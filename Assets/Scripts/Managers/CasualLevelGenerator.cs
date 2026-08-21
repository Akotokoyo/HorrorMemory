using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CasualLevelGenerator : MonoBehaviour
{
    private static CasualLevelGenerator _instance;
    public static CasualLevelGenerator Instance => _instance;

    private const string ConfigResourcePath = "PlayModeLevels/PlayModeLevels";

    private CasualLevelConfigJson config;

    public int fixedSeed = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    public IEnumerator GenerateLevelAsync(Action<RuntimeLevelData> onComplete, Action<string> onError)
    {
        if (fixedSeed >= 0)
        {
            UnityEngine.Random.InitState(fixedSeed);
        }

        if (!TryLoadConfig(out string configError))
        {
            onError?.Invoke(configError);
            yield break;
        }

        LevelConfig levelConfig = PickRandomLevel();
        if (levelConfig == null)
        {
            onError?.Invoke("Play mode: nessun livello abilitato con differenze definite nel JSON.");
            yield break;
        }

        LevelData source = GameManager.Instance.GetLevelConfiguration(levelConfig.id);
        if (source == null)
        {
            onError?.Invoke($"Play mode: nessun LevelData con levelId {levelConfig.id} tra le configurazioni.");
            yield break;
        }

        CasualDifficultyJson difficulty = PickRandomDifficulty();
        if (difficulty == null)
        {
            onError?.Invoke("Play mode: nessuna difficolta' definita nel JSON.");
            yield break;
        }

        RuntimeLevelData level = BuildEmptyLevel(source, difficulty);

        // Seleziona N indici casuali tra le differenze disponibili
        List<int> shuffledIndices = ShuffledIndices(source.differences.Count);
        int countToFind = Mathf.Min(difficulty.differencesToFind, source.differences.Count);

        int enabledCount = 0;
        for (int i = 0; i < countToFind; i++)
        {
            int slotIndex = shuffledIndices[i];
            DifferenceInfo slot = source.differences[slotIndex];


            level.differences[slotIndex].mustBeFound = true;
            level.differences[slotIndex].startedSprite = slot.startedSprite;
            enabledCount++;
        }

        if (enabledCount == 0)
        {
            onError?.Invoke($"Play mode: nessuna differenza abilitata per il livello {levelConfig.id}.");
            yield break;
        }

        if (enabledCount < difficulty.differencesToFind)
        {
            Debug.LogWarning($"Play mode: richieste {difficulty.differencesToFind} differenze per il livello {levelConfig.id}, ma disponibili: {enabledCount}.");
        }

        onComplete?.Invoke(level);
    }

    private bool TryLoadConfig(out string error)
    {
        if (config != null)
        {
            error = null;
            return true;
        }

        TextAsset configAsset = Resources.Load<TextAsset>(ConfigResourcePath);
        if (configAsset == null)
        {
            error = $"Play mode: configurazione non trovata in Resources/{ConfigResourcePath}.json";
            return false;
        }

        try
        {
            config = JsonConvert.DeserializeObject<CasualLevelConfigJson>(configAsset.text);
        }
        catch (Exception e)
        {
            config = null;
            error = $"Play mode: Errore parsing JSON: {e.Message}";
            return false;
        }

        if (config == null || config.levels == null || config.levels.Count == 0)
        {
            config = null;
            error = "Play mode: il JSON non contiene livelli.";
            return false;
        }

        if (config.difficulties == null || config.difficulties.Count == 0)
        {
            config = null;
            error = "Play mode: il JSON non contiene difficolta'.";
            return false;
        }

        error = null;
        return true;
    }

    private LevelConfig PickRandomLevel()
    {
        List<LevelConfig> usable = new List<LevelConfig>();
        for (int i = 0; i < config.levels.Count; i++)
        {
            LevelConfig candidate = config.levels[i];
            if (candidate != null && candidate.enabled)
            {
                LevelData data = GameManager.Instance.GetLevelConfiguration(candidate.id);
                if (data != null && data.differences != null && data.differences.Count > 0)
                {
                    usable.Add(candidate);
                }
            }
        }

        return usable.Count == 0 ? null : usable[UnityEngine.Random.Range(0, usable.Count)];
    }

    private CasualDifficultyJson PickRandomDifficulty()
    {
        float totalWeight = 0f;
        for (int i = 0; i < config.difficulties.Count; i++)
        {
            totalWeight += Mathf.Max(0f, config.difficulties[i].pickWeight);
        }

        if (totalWeight <= 0f)
        {
            return config.difficulties[UnityEngine.Random.Range(0, config.difficulties.Count)];
        }

        float roll = UnityEngine.Random.value * totalWeight;
        for (int i = 0; i < config.difficulties.Count; i++)
        {
            roll -= Mathf.Max(0f, config.difficulties[i].pickWeight);
            if (roll <= 0f)
            {
                return config.difficulties[i];
            }
        }

        return config.difficulties[config.difficulties.Count - 1];
    }

    private static List<int> ShuffledIndices(int count)
    {
        List<int> indices = new List<int>(count);
        for (int i = 0; i < count; i++) indices.Add(i);
        for (int i = indices.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int tmp = indices[i];
            indices[i] = indices[j];
            indices[j] = tmp;
        }
        return indices;
    }

    private static RuntimeLevelData BuildEmptyLevel(LevelData source, CasualDifficultyJson difficulty)
    {
        RuntimeLevelData level = new RuntimeLevelData
        {
            levelName = source.levelName,
            levelId = Constants.PLAY_MODE_LEVEL_ID,
            difficulty = difficulty.level,
            timeLimit = difficulty.timeLimit,
            storyIntroText = string.Empty,
            storyEndingText = string.Empty,
            originalSprite = source.originalSprite,
            modifiedSprite = source.modifiedSprite,
            distortedSprite = source.distortedSprite,
            completionSound = source.completionSound
        };

        // Si clonano tutti gli slot dello ScriptableObject, spenti: scrivere sui
        // DifferenceInfo originali modificherebbe l'asset del livello su disco.
        for (int i = 0; i < source.differences.Count; i++)
        {
            DifferenceInfo original = source.differences[i];
            level.differences.Add(new DifferenceInfo
            {
                normalizedPosition = original.normalizedPosition,
                width = original.width,
                height = original.height,
                startedSprite = null,
                mustBeFound = false
            });
        }

        return level;
    }
}
