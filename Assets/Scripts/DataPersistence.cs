using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataPersistence : MonoBehaviour
{
    public static DataPersistence Instance;
    public string playerName;
    public string bestPlayer;
    public int bestScore;

    private void Awake()
    {
        if(Instance != null) { Destroy(gameObject);return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateLeaderboard(int score)
    {
        if (score > bestScore)
        {
            bestScore = score;
            bestPlayer = playerName;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string bestPlayer;
        public int bestScore;
    }

    public void SaveScore()
    {
        var data = new SaveData();
        data.bestPlayer = playerName;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/safile.json", json);
    }

    public void LoadScore()
    {
        var path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.bestPlayer;
            bestScore = data.bestScore;
        }

    }

}
