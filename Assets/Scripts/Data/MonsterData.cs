using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterData : ILoader<int, Monster>
{
    public List<Monster> monsters = new List<Monster>();

    public Dictionary<int, Monster> MakeDict()
    {
        Dictionary<int, Monster> dict = new Dictionary<int, Monster>();
        foreach (Monster monster in monsters)
            dict.Add(monster.StageLevel, monster);
        return dict;
    }
}

[Serializable]
public class Monster
{
    public int StageLevel;
    public int MaxHp;
    public int Hp;
    public int Attack;
    public float Speed;
}
