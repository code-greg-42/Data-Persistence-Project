using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string currentPlayerName;
    public int currentPlayerBestScore;

    public List<PlayerData> playersData = new List<PlayerData>(); // player data kept here

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadDataFromFile();
    }

    private void OnApplicationQuit()
    {
        SaveDataToFile();
    }

    public void SavePlayerData(string name, int score)
    {
        var player = playersData.Find(player => player.playerName == name);
        if (player != null)
        {
            // update existing score in list
            player.bestScore = score;
        }
        else
        {
            // add new player data to list
            playersData.Add(new PlayerData(name, score));
        }

        // save data to persistent data storage
        SaveDataToFile();
    }

    public void LoadPlayerData(string name)
    {
        var player = playersData.Find(p => p.playerName == name);
        if (player != null)
        {
            currentPlayerName = player.playerName;
            currentPlayerBestScore = player.bestScore;
        }
        else
        {
            currentPlayerName = name;
            currentPlayerBestScore = 0;
        }
    }

    public void SaveDataToFile()
    {
        var data = new GameData(playersData, currentPlayerName);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
    }

    public void LoadDataFromFile()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);
            playersData = data.players;
            LoadPlayerData(data.lastActivePlayer);
        }
    }

    // class for organized player data, with constructor for adding new players
    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int bestScore;

        public PlayerData(string name, int score)
        {
            playerName = name;
            bestScore = score;
        }
    }

    // class for including the last active player, so the session can start with their high score shown
    [System.Serializable]
    private class GameData
    {
        public List<PlayerData> players;
        public string lastActivePlayer;

        public GameData(List<PlayerData> players, string lastActivePlayer)
        {
            this.players = players;
            this.lastActivePlayer = lastActivePlayer;
        }
    }
}
