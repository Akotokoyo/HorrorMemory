using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CasualLevelGenerator : MonoBehaviour
{
    [SerializeField] private string configFileName = "CasualLevel_Example.json";
    [SerializeField] private int fixedSeed = -1;

    private static CasualLevelGenerator _instance;
    public static CasualLevelGenerator Instance => _instance;

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
            UnityEngine.Random.InitState(fixedSeed);

        string path = Path.Combine(Application.streamingAssetsPath, configFileName);
        string json;
        try
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            // StreamingAssets su Android richiede UnityWebRequest
            var www = new UnityEngine.Networking.UnityWebRequest(path);
            www.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            if (www.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                onError?.Invoke($"Errore lettura JSON: {www.error}");
                yield break;
            }
            json = www.downloadHandler.text;
#else
            json = File.ReadAllText(path);
#endif
        }
        catch (Exception e)
        {
            onError?.Invoke($"Errore lettura JSON: {e.Message}");
            yield break;
        }

        CasualLevelConfigJson config;
        try
        {
            config = JsonUtility.FromJson<CasualLevelConfigJson>(json);
        }
        catch (Exception e)
        {
            onError?.Invoke($"Errore parsing JSON: {e.Message}");
            yield break;
        }

        if (config.differenceSlots == null || config.differenceSlots.Length == 0)
        {
            onError?.Invoke("Nessuno slot di differenza nel JSON");
            yield break;
        }

        int countToFind = Mathf.Clamp(
            UnityEngine.Random.Range(config.minDifferencesToFind, config.maxDifferencesToFind + 1),
            1, config.differenceSlots.Length);

        int[] indices = new int[config.differenceSlots.Length];
        for (int i = 0; i < indices.Length; i++) indices[i] = i;
        Shuffle(indices);
        HashSet<int> activeSlots = new HashSet<int>();
        for (int i = 0; i < countToFind; i++)
            activeSlots.Add(indices[i]);

        var diffData = new List<(CasualDifferenceSlotJson slot, bool mustBeFound, string startedAddr, string distortedAddr)>();
        var spriteAddresses = new HashSet<string> { config.originalImageAddress, config.distortedImageAddress };

        for (int i = 0; i < config.differenceSlots.Length; i++)
        {
            var slot = config.differenceSlots[i];
            bool mustBeFound = activeSlots.Contains(i);
            if (slot.startedSpriteAddresses == null || slot.startedSpriteAddresses.Length == 0 ||
                slot.distortedSpriteAddresses == null || slot.distortedSpriteAddresses.Length == 0)
            {
                Debug.LogWarning($"Slot {i}: missing sprite addresses, skipped");
                continue;
            }
            string startedAddr = slot.startedSpriteAddresses[UnityEngine.Random.Range(0, slot.startedSpriteAddresses.Length)];
            string distortedAddr = slot.distortedSpriteAddresses[UnityEngine.Random.Range(0, slot.distortedSpriteAddresses.Length)];
            diffData.Add((slot, mustBeFound, startedAddr, distortedAddr));
            spriteAddresses.Add(startedAddr);
            spriteAddresses.Add(distortedAddr);
        }

        var loadedSprites = new Dictionary<string, Sprite>();
        foreach (string addr in spriteAddresses)
        {
            if (string.IsNullOrEmpty(addr)) continue;
            var op = Addressables.LoadAssetAsync<Sprite>(addr);
            yield return op;
            if (op.Status == AsyncOperationStatus.Succeeded)
                loadedSprites[addr] = op.Result;
            else
                Debug.LogWarning($"Impossibile caricare sprite: {addr}");
        }

        var loadedClips = new Dictionary<string, AudioClip>();
        if (!string.IsNullOrEmpty(config.ambientSoundAddress))
        {
            var op = Addressables.LoadAssetAsync<AudioClip>(config.ambientSoundAddress);
            yield return op;
            if (op.Status == AsyncOperationStatus.Succeeded)
                loadedClips[config.ambientSoundAddress] = op.Result;
        }
        if (!string.IsNullOrEmpty(config.completionSoundAddress))
        {
            var op = Addressables.LoadAssetAsync<AudioClip>(config.completionSoundAddress);
            yield return op;
            if (op.Status == AsyncOperationStatus.Succeeded)
                loadedClips[config.completionSoundAddress] = op.Result;
        }

        var levelData = new RuntimeLevelData
        {
            levelName = config.levelName,
            timeLimit = config.timeLimit,
            scoreAddTime = config.scoreAddTime,
            waitingtime = config.waitingtime,
            difficulty = (DifficultyLevel)Mathf.Clamp(config.difficulty, 0, 2),
            originalSprite = loadedSprites.TryGetValue(config.originalImageAddress, out var os) ? os : null,
            distortedSprite = loadedSprites.TryGetValue(config.distortedImageAddress, out var ds) ? ds : null,
            ambientSound = loadedClips.TryGetValue(config.ambientSoundAddress, out var asc) ? asc : null,
            completionSound = loadedClips.TryGetValue(config.completionSoundAddress, out var csc) ? csc : null,
        };

        if (levelData.originalSprite == null || levelData.distortedSprite == null)
        {
            onError?.Invoke("Impossibile caricare immagini originali/distorte");
            yield break;
        }

        foreach (var (slot, mustBeFound, startedAddr, distortedAddr) in diffData)
        {
            if (!loadedSprites.TryGetValue(startedAddr, out var started) || !loadedSprites.TryGetValue(distortedAddr, out var distorted))
            {
                Debug.LogWarning($"Sprite mancanti per slot pos=({slot.posX},{slot.posY})");
                continue;
            }
            levelData.differences.Add(new DifferenceInfo
            {
                normalizedPosition = new Vector2(slot.posX, slot.posY),
                width = slot.width,
                height = slot.height,
                deflectinRadious = slot.deflectinRadious,
                startedSprite = started,
                distortedSprite = distorted,
                mustBeFound = mustBeFound
            });
        }

        onComplete?.Invoke(levelData);
    }

    private static void Shuffle<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}
