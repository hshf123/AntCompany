using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Arrow> ArrowDict { get; private set; } = new Dictionary<int, Arrow>();
    public Dictionary<int, Boss> BossDict { get; private set; } = new Dictionary<int, Boss>();
    public BowData BowData { get; set; } = new BowData();
    public Dictionary<int, Monster> MonsterDict { get; private set; } = new Dictionary<int, Monster>();
    public Dictionary<int, Player> PlayerDict { get; private set; } = new Dictionary<int, Player>();
    public Dictionary<int, Stage> StageDict { get; private set; } = new Dictionary<int, Stage>();

    public void Init()
    {
        ArrowDict = LoadJson<ArrowData, int, Arrow>("ArrowData").MakeDict();
        BossDict = LoadJson<BossData, int, Boss>("BossData").MakeDict();
        BowData = LoadJson<BowData>("BowData");
        MonsterDict = LoadJson<MonsterData, int, Monster>("MonsterData").MakeDict();
        PlayerDict = LoadJson<PlayerData, int, Player>("PlayerData").MakeDict();
        StageDict = LoadJson<StageData, int, Stage>("StageData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset json = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(json.text);
    }
    Loader LoadJson<Loader>(string path)
    {
        TextAsset json = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(json.text);
    }

    public void Save(SaveData save)
    {
        string path = Path.Combine(Application.persistentDataPath, "SaveData.json");
        string json = JsonUtility.ToJson(save, true);
        File.WriteAllText(path, json);
        Debug.Log($"Save : {Application.persistentDataPath} SaveData.json");
    }
    public SaveData LoadSaveData()
    {
        string path = Application.persistentDataPath + "/SaveData.json";
        if (File.Exists(path) == false)
        {
            Debug.Log("Failed to load save data");
            return null;
        }

        string fileStr = File.ReadAllText(path);

        return JsonUtility.FromJson<SaveData>(fileStr);
    }
}
