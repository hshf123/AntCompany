using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DataManager
{
    public MonsterData Monster;
    public PlayerData Player;
    public Stage1Data Stage1;

    public void Init()
    {
        InitMonsterData();
        InitPlayerData();
        InitStage1Data();
    }

    void InitMonsterData()
    {
        Monster = new MonsterData();
        Monster.Attack = 10;
        Monster.MaxHp = 100;
        Monster.Hp = 100;
    }
    void InitPlayerData()
    {
        Player = new PlayerData();
        Player.Level = 1;
        Player.Exp = 0;
        Player.Money = 100000;

        Player.Hp = 1500;
        Player.Attack = 20;
    }
    void InitStage1Data()
    {
        Stage1 = new Stage1Data();
        Stage1.MonsterCount = 5;
    }

    //void ReadXMLData()
    //{
    //    XmlReaderSettings xmlReaderSettings = new XmlReaderSettings()
    //    {
    //        IgnoreComments = true,      // �ּ� ����
    //        IgnoreWhitespace = true     // ���� ����
    //    };

    //    using (XmlReader r = XmlReader.Create("Assets/Resources/Data/StartData.xml", xmlReaderSettings))
    //    {
    //        // ���� r.Dispose()�� �ؼ� �ݾ���� �ϴµ� using�� ����ϸ� Dispose�� �ڵ����� ���ش�.
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
