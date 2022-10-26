using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : ILoader<int, Player>
{
    public List<Player> players = new List<Player>();

    public Dictionary<int, Player> MakeDict()
    {
        Dictionary<int, Player> dict = new Dictionary<int, Player>();
        foreach (Player player in players)
            dict.Add(player.Level, player);
        return dict;
    }
}

[Serializable]
public class Player
{
    public int Level;
    public int MaxHp;
    public int Hp;
    public int Attack;
    public float AttackSpeed;
}
