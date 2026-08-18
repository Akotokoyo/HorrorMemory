using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CasualLevelGenerator : MonoBehaviour
{
    private const string ConfigResourcePath = "PlayModeLevels/PlayModeLevels";

    [Tooltip("Valore >= 0 rende la generazione riproducibile. Lasciare -1 in produzione.")]
    [SerializeField] private int fixedSeed = -1;

    private static CasualLevelGenerator _instance;
    public static CasualLevelGenerator Instance => _instance;

    private CasualLevelConfigJson config;

    // Gli sprite restano caricati finche' si gioca in play mode: le partite successive
    // riusano gli stessi address invece di ricaricarli, e si rilascia tutto all'uscita.
    private readonly Dictionary<string, AsyncOperationHandle<Sprite>> spriteHandles =
        new Dictionary<string, AsyncOperationHandle<Sprite>>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            ReleaseLoadedSprites();
        }
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
            onError?.Invoke("Play mode: no level enabled with defined slots in JSON.");
            yield break;
        }

        LevelData source = GameManager.Instance.GetLevelConfiguration(levelConfig.id);
        if (source == null)
        {
            onError?.Invoke($"Play mode: no LevelData with levelId {levelConfig.id} between the configurations.");
            yield break;
        }

        CasualDifficultyJson difficulty = PickRandomDifficulty();
        if (difficulty == null)
        {
            onError?.Invoke("Play mode: no difficulties defined in JSON.");
            yield break;
        }

        List<CasualLevelDifferenceSlots> candidates = CollectCandidateSlots(levelConfig, source);
        if (candidates.Count == 0)
        {
            onError?.Invoke($"Play mode: The level {levelConfig.id} has not available slots. check on the slotindex and the differencies list");
            yield break;
        }

        RuntimeLevelData level = BuildEmptyLevel(source, difficulty);

        int countToFind = Mathf.Clamp(difficulty.differencesToFind, 1, candidates.Count);
        Shuffle(candidates);

        int enabledCount = 0;
        for (int i = 0; i < candidates.Count && enabledCount < countToFind; i++)
        {
            CasualLevelDifferenceSlots slot = candidates[i];
            string address = slot.sprites[UnityEngine.Random.Range(0, slot.sprites.Count)];

            Sprite sprite = null;
            yield return LoadSprite(address, loaded => sprite = loaded);

            if (sprite == null)
            {
                continue;
            }

            DifferenceInfo difference = level.differences[slot.slotIndex];
            difference.startedSprite = sprite;
            difference.mustBeFound = true;
            enabledCount++;
        }

        if (enabledCount == 0)
        {
            onError?.Invoke($"Play mode: no sprite loaded for the level {levelConfig.id}. Verify the address on the addressables.");
            yield break;
        }

        if (enabledCount < countToFind)
        {
            Debug.LogWarning($"Play mode: request {countToFind} difference on the level {levelConfig.id} but the enabled are: {enabledCount}.");
        }

        onComplete?.Invoke(level);
    }

    public void ReleaseLoadedSprites()
    {
        foreach (AsyncOperationHandle<Sprite> handle in spriteHandles.Values)
        {
            if (handle.IsValid())
            {
                Addressables.Release(handle);
            }
        }
        spriteHandles.Clear();
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
            error = $"Play mode: configuration not found in Resources/{ConfigResourcePath}.json";
            return false;
        }

        try
        {
            config = JsonConvert.DeserializeObject<CasualLevelConfigJson>(configAsset.text);
        }
        catch (Exception e)
        {
            config = null;
            error = $"Play mode: Parsing Json Error: {e.Message}";
            return false;
        }

        if (config == null || config.levels == null || config.levels.Count == 0)
        {
            config = null;
            error = "Play mode: JSON doesn't contain any level";
            return false;
        }

        if (config.difficulties == null || config.difficulties.Count == 0)
        {
            config = null;
            error = "Play mode: There are not difficulties on the JSON";
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
            if (candidate != null && candidate.enabled && candidate.differenceSlots != null && candidate.differenceSlots.Count > 0)
            {
                usable.Add(candidate);
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

    private static List<CasualLevelDifferenceSlots> CollectCandidateSlots(LevelConfig levelConfig, LevelData source)
    {
        List<CasualLevelDifferenceSlots> candidates = new List<CasualLevelDifferenceSlots>();
        HashSet<int> alreadySeen = new HashSet<int>();

        for (int i = 0; i < levelConfig.differenceSlots.Count; i++)
        {
            CasualLevelDifferenceSlots slot = levelConfig.differenceSlots[i];

            if (slot == null || slot.sprites == null || slot.sprites.Count == 0)
            {
                Debug.LogWarning($"Play mode: level {levelConfig.id}, slot in position {i} with no sprites, ignored.");
                continue;
            }

            if (slot.slotIndex < 0 || slot.slotIndex >= source.differences.Count)
            {
                Debug.LogWarning($"Play mode: level {levelConfig.id}, slotIndex {slot.slotIndex} out of {source.differences.Count} slot of '{source.name}', ignored.");
                continue;
            }

            if (!alreadySeen.Add(slot.slotIndex))
            {
                Debug.LogWarning($"Play mode: level {levelConfig.id}, slotIndex {slot.slotIndex} duplicated in JSON, The second occurance will be ignored.");
                continue;
            }

            candidates.Add(slot);
        }

        return candidates;
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

    private IEnumerator LoadSprite(string address, Action<Sprite> onLoaded)
    {
        if (spriteHandles.TryGetValue(address, out AsyncOperationHandle<Sprite> cached))
        {
            onLoaded?.Invoke(cached.IsValid() ? cached.Result : null);
            yield break;
        }

        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(address);
        yield return handle;

        if (handle.Status != AsyncOperationStatus.Succeeded || handle.Result == null)
        {
            Debug.LogWarning($"Play mode: Impossible to load the address '{address}'.");
            Addressables.Release(handle);
            onLoaded?.Invoke(null);
            yield break;
        }

        spriteHandles[address] = handle;
        onLoaded?.Invoke(handle.Result);
    }

    private static void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
