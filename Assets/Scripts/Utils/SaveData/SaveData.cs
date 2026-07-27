using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class SaveData
{
    // Create a field for the save file.
    string gamePath;

    // FileStream used for reading and writing files.
    FileStream dataStream;

    public void WriteFile(JsonConverter jsonConverter, GameData gameData)
    {
#if UNITY_EDITOR
        gamePath = $"Assets/Data/GameData.sav";
#else
        gamePath = $"{Application.persistentDataPath}.sav";
#endif
#if !UNITY_EDITOR
            Debug.Log("NOT EDITOR");
            // Create new AES instance.
            Aes iAes = Aes.Create();

            // Update the internal key.
            byte[] savedKey = iAes.Key;

            // Convert the byte[] into a Base64 String.
            string key = System.Convert.ToBase64String(savedKey);

            // Update the PlayerPrefs
            PlayerPrefs.SetString($"key", key);

            // Create a FileStream for creating files.
            dataStream = new FileStream(gamePath, FileMode.Create);

            // Save the new generated IV.
            byte[] inputIV = iAes.IV;

            // Write the IV to the FileStream unencrypted.
            dataStream.Write(inputIV, 0, inputIV.Length);

            // Create CryptoStream, wrapping FileStream.
            CryptoStream iStream = new CryptoStream(
                    dataStream,
                    iAes.CreateEncryptor(iAes.Key, iAes.IV),
                    CryptoStreamMode.Write);

            // Create StreamWriter, wrapping CryptoStream.
            StreamWriter sWriter = new StreamWriter(iStream);

            string jsonString = jsonConverter.WriteToJsonFromObject(gameData);

            // Write to the innermost stream (which will encrypt).
            sWriter.Write(jsonString);

            sWriter.Close();
            iStream.Close();
            dataStream.Close();
#else
        string jsonString = jsonConverter.WriteToJsonFromObject(gameData);
        File.WriteAllText(gamePath, jsonString);
#endif
    }

    public GameData ReadSaveFile()
    {
#if UNITY_EDITOR
        gamePath = $"Assets/Data/GameData.sav";
#else
        gamePath = $"{Application.persistentDataPath}/GameData.sav";
#endif

#if !UNITY_EDITOR
            // Does the file exist?
            if (File.Exists(gamePath))
            {
                // Create FileStream for opening files.
                dataStream = new FileStream(gamePath, FileMode.Open);

                // Create new AES instance.
                Aes oAes = Aes.Create();

                // Create an array of correct size based on AES IV.
                byte[] outputIV = new byte[oAes.IV.Length];

                // Update key based on PlayerPrefs
                // (Convert the String into a Base64 byte[] array.)
                byte[] savedKey = System.Convert.FromBase64String(PlayerPrefs.GetString($"key"));

                // Read the IV from the file.
                dataStream.Read(outputIV, 0, outputIV.Length);

                // Create CryptoStream, wrapping FileStream
                CryptoStream oStream = new CryptoStream(
                       dataStream,
                       oAes.CreateDecryptor(savedKey, outputIV),
                       CryptoStreamMode.Read);

                // Create a StreamReader, wrapping CryptoStream
                StreamReader reader = new StreamReader(oStream);

                // Read the entire file into a String value.
                string text = reader.ReadToEnd();
                // Always close a stream after usage.
                reader.Close();

                // Deserialize the JSON data 
                //  into a pattern matching the GameData class.
                return JsonConvert.DeserializeObject<GameData>(text);
            }
            return null;

#else
        if (File.Exists(gamePath))
        {
            JsonConverter jsonConverter = new JsonConverter();
            string jsonText = System.IO.File.ReadAllText(gamePath);
            return JsonConvert.DeserializeObject<GameData>(jsonText);
        }
        return null;
#endif
    }

    public void DeleteFile()
    {
#if USE_RESOURCES_DATA
        gamePath = $"Assets/Data/SaveFiles/GameData.sav";
#else
        gamePath = $"{Application.persistentDataPath}.sav";
#endif

        if (File.Exists(gamePath))
        {
            File.Delete(gamePath);
            File.Delete($"{gamePath}.meta");

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}