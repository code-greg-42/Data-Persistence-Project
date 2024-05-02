using System.Collections;
using System.Collections.Generic;
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
    }

    public void SavePlayerData(string name, int score)
    {
        var player = playersData.Find(player => player.playerName == name);
        if (player != null)
        {
            // update existing score
            player.bestScore = score;
        }
        else
        {
            // add new player data
            playersData.Add(new PlayerData(name, score));
        }
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

    // wrapper for being able to serialize lists to json
    [System.Serializable]
    private class Serialization<T>
    {
        [SerializeField]
        private List<T> items;

        public List<T> ToList() => items;

        public Serialization(List<T> items)
        {
            this.items = items;
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
}
