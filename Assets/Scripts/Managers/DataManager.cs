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
    public Dictionary<int, Monster> MonsterDict { get; private set; } = new Dictionary<int, Monster>();
    public Dictionary<int, Player> PlayerDict { get; private set; } = new Dictionary<int, Player>();
    public Dictionary<int, Stage> StageDict { get; private set; } = new Dictionary<int, Stage>();
    public Dictionary<int, Arrow> ArrowDict { get; private set; } = new Dictionary<int, Arrow>();

    public void Init()
    {
        MonsterDict = LoadJson<MonsterData, int, Monster>("MonsterData").MakeDict();
        PlayerDict = LoadJson<PlayerData, int, Player>("PlayerData").MakeDict();
        StageDict = LoadJson<StageData, int, Stage>("StageData").MakeDict();
        ArrowDict = LoadJson<ArrowData, int, Arrow>("ArrowData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset json = Managers.Resource.Load<TextAsset>($"Data/{path}");
        Debug.Log(json.text);
        return JsonUtility.FromJson<Loader>(json.text);
    }

    public void Save(Save save)
    {
        string path = Path.Combine(Application.persistentDataPath, "SaveData.json");
        string json = JsonUtility.ToJson(save, true);
        File.WriteAllText(path, json);
        Debug.Log($"Save : {Application.persistentDataPath} SaveData.json");
    }

    public Save LoadSaveData()
    {
        string path = Application.persistentDataPath + "/SaveData.json";
        if (File.Exists(path) == false)
        {
            Debug.Log("Failed to load save data");
            return null;
        }

        string fileStr = File.ReadAllText(path);

        return JsonUtility.FromJson<Save>(fileStr);
    }

    //void ReadXMLData()
    //{
    //    XmlReaderSettings xmlReaderSettings = new XmlReaderSettings()
    //    {
    //        IgnoreComments = true,      // 주석 무시
    //        IgnoreWhitespace = true     // 공백 무시
    //    };

    //    using (XmlReader r = XmlReader.Create("Assets/Resources/Data/StartData.xml", xmlReaderSettings))
    //    {
    //        // 원래 r.Dispose()를 해서 닫아줘야 하는데 using을 사용하면 Dispose를 자동으로 해준다.
    //        r.MoveToContent();
    //        while (r.Read())
    //        {
    //            if (r.Depth == 1 && r.NodeType == XmlNodeType.Element)
    //            {
    //                for (int i = 0; i < r.AttributeCount; i++)
    //                {
    //                    //Debug.Log($"{r.GetAttribute(i)}");
    //                }
    //            }
    //        }
    //    }

    //}
}
