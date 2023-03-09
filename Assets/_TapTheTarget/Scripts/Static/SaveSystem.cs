using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public static class SaveSystem
{
    private static readonly string SAVE_FILE_NAME = "PlayerProgress.json";

    public static void Save(GameData data)
    {
        Debug.Log("Saving Progress");
        string savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        SavePlayers(data.PlayersList);
    }

    public static GameData Load()
    {
        GameData gameData = ScriptableObject.CreateInstance<GameData>();
        string savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            JsonUtility.FromJsonOverwrite(json, gameData);
        }
        else
        {
            Debug.LogWarning($"Saved player was not found at path: {savePath} \nLoaded defualt data");
            gameData = Resources.Load<GameData>("DefaultGameData");
            Save(gameData);
        }

        gameData.PlayersList = LoadPlayers();
        gameData.ActivePlayer = gameData.PlayersList[0];
        gameData.ActiveMode = GameMode.Easy;
        return gameData;
    }

    public static void SavePlayers(List<PlayerData> players)
    {
        string playersFloderPath = Application.persistentDataPath + "/Players";
        if (!Directory.Exists(playersFloderPath))
        {
            Directory.CreateDirectory(playersFloderPath);
        }

        foreach (PlayerData player in players) 
        {
            string json = JsonConvert.SerializeObject(player);
            string savePath = Path.Combine(playersFloderPath, "Player_" + player.userName);
            File.WriteAllText(savePath, json);
        }
    }

    public static List<PlayerData> LoadPlayers()
    {
        string playersFloderPath = Application.persistentDataPath + "/Players";
        string[] filePaths = Directory.GetFiles(playersFloderPath);
        List<PlayerData> players = new();  
        foreach (string filePath in filePaths)
        {
            string json = File.ReadAllText(filePath);
            PlayerData player = JsonConvert.DeserializeObject<PlayerData>(json);
            players.Add(player);
        }
        return players;
    }
}