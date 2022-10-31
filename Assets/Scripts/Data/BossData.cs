using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BossData : ILoader<int, Boss>
{
    public List<Boss> bosses = new List<Boss>();

    public Dictionary<int, Boss> MakeDict()
    {
        Dictionary<int, Boss> dict = new Dictionary<int, Boss>();
        foreach (Boss boss in bosses)
            dict.Add(boss.Level, boss);
        return dict;
    }
}

[Serializable]
public class Boss
{
    public int Level;
    public int MaxHp;
    public int Hp;
    public int Attack;
    public float Speed;
}
